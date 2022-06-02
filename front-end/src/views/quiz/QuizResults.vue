<template>
    <div v-if="!loading" class="card">
        <div class="mb-3">
            <h1 class="mt-3">{{ quiz.title }}</h1>
            <hr class="border-dark">
            <div v-if="quiz.quizOrPoll === 'Q'">
                <h2>Your score: </h2>
                <h3 class="text-primary">{{quizzer.points}}p / {{quiz.pointsSum}}p</h3>
            </div>
            <div class="progress-outer">
                <div class="progress">
                    <div class="progress-bar progress-bar-danger progress-bar-striped active" v-bind:style="'width:' + quizzer.quizPercentage + '%'"
                         style="box-shadow:-1px 10px 10px rgba(240, 173, 78,0.7);"></div>
                    <div class="progress-value">{{quizzer.quizPercentage}}%</div>
                </div>
            </div>
        </div>
        <div v-if="quiz.showAnswers">
            <div v-for="question of questions" v-bind:key="question" class="card card-hover shadow mb-4">
                <div class="card-body p-5">
                    <h4 class="mb-4">{{ question.questionText }}</h4>
                    <h5 v-if="quiz.quizOrPoll === 'Q'">{{ question.points }}</h5>
                    <ul class="list-unstyled list pl-5">
                        <li v-for="answer of answers.filter(a => a.questionId === question.id)" v-bind:key="answer" class="mb-3">
                            <i v-bind:class="bulletStyle(quiz, answer)" class="fa mr-3 float-left"></i>
                            <h6 class="text-left">
                                <span v-if="quizzerAnswers.find(q => q.answerId === answer.id)" class="text-primary">{{ answer.answerText }}</span>
                                <span v-else>{{ answer.answerText }}</span>
                                <span v-if="quizzerAnswers.find(q => q.answerId === answer.id && q.quizzerId === quizzer.id)" class="badge badge-primary badge-pill float-right" >Your choice</span>
                            </h6>
                            <img class="float-left" v-if="answer.url" v-bind:src="answer.url" width="100" height="100">
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        <div v-else>
            Showing further answers has been disabled by the author.
        </div>
        <div>
            <router-link class="mx-4 text-primary" to="/quizzes">Back to quizzes</router-link>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import { BaseService } from '@/services/base-service'
import IQuizzer from '@/domain/IQuizzer'
import store from '@/store'
import IQuestion from '@/domain/IQuestion'
import IQuizzerAnswer from '@/domain/IQuizzerAnswer'
import IAnswer from '@/domain/IAnswer'
import IQuiz from '@/domain/IQuiz'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class QuizResults extends Vue {
    id!: string;
    quizzer!: IQuizzer;
    quiz!: IQuiz;

    questions: IQuestion[] = []
    quizzerAnswers: IQuizzerAnswer[] = []
    answers: IAnswer[] = []

    loading: boolean = true;

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    bulletStyle(quiz: IQuiz, answer: IAnswer): string {
        if (quiz.quizOrPoll !== 'Q') return 'fa-circle text-dark fa-xs';
        if (answer.isCorrect) return 'fa-check text-success';
        else return 'fa-times text-danger';
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quizzer = data.data!;
            }
        });
        const questionService = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await questionService.getAll().then((data) => {
            if (data.ok) {
                this.questions = data.data!.filter(q => q.totalAnswers > 1 && q.quizId === this.quizzer.quizId);
            } else {
                console.log(data)
            }
        });
        const quizService = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await quizService.get(this.quizzer.quizId).then((data) => {
            if (data.ok) {
                this.quiz = data.data!;
            } else {
                console.log(data)
            }
        });
        const quizzerAnswerService = new BaseService<IQuizzerAnswer>('v1/quizzerAnswers', this.token || undefined);
        await quizzerAnswerService.getAll().then((data) => {
            if (data.ok) {
                this.quizzerAnswers = data.data!;
            } else {
                console.log(data)
            }
        });
        const answerService = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await answerService.getAll().then((data) => {
            if (data.ok) {
                this.answers = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>
.progress-outer{
    background: #fff;
    border-radius: 50px;
    padding: 25px;
    margin: 10px 0;
    box-shadow: 0 0  10px rgba(209, 219, 231,0.7);
}
.progress{
    height: 27px;
    margin: 0;
    overflow: visible;
    border-radius: 50px;
    background: #eaedf3;
    box-shadow: inset 0 10px  10px rgba(244, 245, 250,0.9);
}
.progress .progress-bar{
    border-radius: 50px;
}
.progress .progress-value{
    position: relative;
    left: -45px;
    top: 4px;
    font-size: 14px;
    font-weight: bold;
    color: #fff;
    letter-spacing: 2px;
}
.progress-bar.active{
    animation: reverse progress-bar-stripes 0.40s linear infinite, animate-positive 2s;
}
@-webkit-keyframes animate-positive{
    0% { width: 0%; }
}
@keyframes animate-positive {
    0% { width: 0%; }
}
</style>
