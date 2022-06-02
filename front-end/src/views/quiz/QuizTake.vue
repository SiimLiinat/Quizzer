<template>
    <div v-if="!loading" class="card card-body">
        <div class="mb-3">
            <h1>{{ quiz.title }}</h1>
            <hr class="border-dark">
        </div>
        <div v-if="quiz.timeLimit" class="align-self-center position-fixed" style="top: 45%; left: 90%">
            <base-timer v-once v-bind:timer="timeLeft.seconds"></base-timer>
        </div>
        <div v-for="question of questions" v-bind:key="question" class="card card-hover shadow mb-4">
            <div class="card-body p-5">
                <img v-if="question.url" :src="question.url" width="300" height="300" class="mw-100 mh-100">
                <h4 class="mb-4">{{ question.questionText }}</h4>
                <h6 v-if="quiz.quizOrPoll === 'Q'" class="mb-4 mt-n2">{{ question.points || 1 }} point(s)</h6>
                <hr>
                <ul class="list-unstyled list pl-5">
                    <li v-for="answer of answers.filter(a => a.questionId === question.id)" v-bind:key="answer" class="mb-3 text-left">
                        <input type="radio" :name="question.id" :value="answer.id" required v-model="answerCollection[question.id]">
                        <label :for="answer.id">{{ answer.answerText }}</label>
                        <img class="float-right mw-100 mh-100" v-if="answer.url" v-bind:src="answer.url" width="300" height="300">
                    </li>
                </ul>
            </div>
        </div>
        <button class="btn btn-primary" @click="submitAnswers">Submit</button>
    </div>
    <div v-else>
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store'
import { BaseService } from '@/services/base-service'
import IQuestion from '@/domain/IQuestion'
import IAnswer from '@/domain/IAnswer'
import IQuiz from '@/domain/IQuiz'
import IQuizzer from '@/domain/IQuizzer'
import IQuizzerAdd from '@/domain/IQuizzerAdd'
import BaseTimer from '@/components/BaseTimer.vue'
import IQuizzerAnswerAdd from '@/domain/IQuizzerAnswerAdd'
@Options({
    components: {
        BaseTimer
    },
    props: {
        id: String,
    }
})
export default class QuizTake extends Vue {
    id!: string;
    loading: boolean = true;
    quiz!: IQuiz;
    quizzer!: IQuizzer;
    questions: IQuestion[] = [];
    answers: IAnswer[] = [];

    timeLeft: { seconds: number | undefined } = { seconds: undefined };

    answerCollection: { [id: string]: string | undefined } = {}

    get token(): string | undefined {
        return store.state.token;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        if (!this.userId) await this.$router.push("/login");
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quiz = data.data!;
            }
        });
        const quizzerService = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await quizzerService.getAll({ userId: this.userId }).then((data) => {
            if (data.ok) {
                const quizzers: IQuizzer[] = data.data!.filter(q => q.quizId === this.quiz.id && q.appUserId === this.userId);
                if (quizzers.find(q => q.quizId && !this.quiz.repeatable && this.role !== 'Admin' && this.quiz.appUserId !== this.userId)) this.$router.push("/");
                const oldQuizzer: IQuizzer | undefined = quizzers.find(q => !q.finishedAt && (!this.quiz.timeLimit || Date.parse(q!.startedAt) > (Date.now() - this.quiz.timeLimit * 600000)));
                if (oldQuizzer !== undefined && this.quiz.timeLimit) {
                    this.timeLeft.seconds = Math.round((Date.parse(oldQuizzer!.startedAt) + this.quiz.timeLimit * 60000 - Date.now()) / 1000);
                }
                if (this.timeLeft.seconds && this.timeLeft.seconds < 0) {
                    this.quizzer = oldQuizzer!;
                    this.submitAnswers();
                }
                if (oldQuizzer !== undefined) {
                    this.quizzer = oldQuizzer;
                }
            }
        });
        console.log(this.quizzer)
        if (!this.quizzer) {
            let quizzerId: any;
            const quizzerAddService = new BaseService<IQuizzerAdd>('v1/quizzers', this.token || undefined);
            const quizzerAdd: IQuizzerAdd = { quizId: this.quiz.id, appUserId: this.userId! }
            await quizzerAddService.post(quizzerAdd).then((data) => {
                if (data.ok) {
                    quizzerId = data.data!;
                }
            });
            if (quizzerId) {
                await quizzerService.get(quizzerId).then((data) => {
                    if (data.ok) {
                        this.quizzer = data.data!;
                        if (this.quiz.timeLimit) this.timeLeft.seconds = this.quiz.timeLimit * 60;
                    }
                });
            }
        }
        const questionService = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await questionService.getAll({ quizId: this.quiz.id }).then((data) => {
            if (data.ok) {
                this.questions = data.data!
                    .filter(q => q.totalAnswers > 1 && (q.hasCorrectAnswer || this.quiz.quizOrPoll === 'P'))
                    .sort((a, b) => (a.questionNumber || Number.MAX_SAFE_INTEGER) > (b.questionNumber || Number.MAX_SAFE_INTEGER) ? 1 : -1);
                for (const question of this.questions) {
                    this.answerCollection[question.id] = undefined;
                }
            }
        });
        const answerService = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await answerService.getAll({ quizId: this.quiz.id }).then((data) => {
            if (data.ok) {
                this.answers = data.data!;
            }
        });
        const timer = setInterval(() => {
            this.countTime();
            if (this.timeLeft.seconds === 0) clearInterval(timer);
        }, 1000)
        this.loading = false;
    }

    countTime(): void {
        this.timeLeft.seconds!--;
        if (this.timeLeft.seconds === 0) this.submitAnswers();
    }

    async submitAnswers(): Promise<void> {
        let points = 0;
        const quizzerAnswerService = new BaseService<IQuizzerAnswerAdd>('v1/quizzerAnswers', this.token || undefined);
        for (const questionId in this.answerCollection) {
            const answer = this.answers.find(a => a.id === this.answerCollection[questionId])
            const question = this.questions.find(q => q.id === questionId);
            if (answer && answer!.isCorrect) points = points + (question!.points || 1);

            if (answer) {
                const quizzerAnswer: IQuizzerAnswerAdd = { quizzerId: this.quizzer!.id, answerId: answer!.id }
                await quizzerAnswerService.post(quizzerAnswer).then((data) => {
                    console.log(data)
                });
            }
        }
        this.quizzer!.points = points;
        const quizzerService = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await quizzerService.put(this.quizzer!.id, this.quizzer!).then((data) => {
            if (data.ok) {
                this.$router.push('/quiz/results/' + this.quizzer!.id)
            }
        });
    }
}
</script>

<style scoped>

</style>
