<template>
    <div v-if="!loading" class="card card-body">
        <div class="mb-3">
            <h1>{{ quiz.title }}</h1>
            <button @click="modal = !modal" type="button" class="btn btn-primary btn-sm mb-3">Edit</button>
            <quiz-edit v-if="modal" :id="quiz.id" :refreshPage="true"></quiz-edit>
            <hr class="border-dark">
        </div>
        <div class="mb-3">
            <h2>Questions: </h2>
        </div>
        <button @click="questionModal = !questionModal" type="button" class="btn btn-outline-secondary btn-sm mb-3 w-25 align-self-center">Add question</button>
        <question-create v-if="questionModal" :setQuizId="quiz.id" :return="true"></question-create>
        <div v-for="question of questions" v-bind:key="question" class="card card-hover shadow mb-4">
            <div class="card-body p-5">
                <img v-if="question.url" v-bind:src="question.url" width="300" height="300">
                <h4 class="mb-4"><router-link :to="'/question/edit/' + question.id">{{ question.questionText }}</router-link><a @click="deleteQuestion(question.id)" class="fa mr-3 text-primary fa-times text-danger justify-content-end"></a></h4>
                <h5 class="mt-n3 mb-5">Total answers: {{ question.totalQuizzerAnswers}}</h5>
                <hr class="border-dark">
                <ul class="list-unstyled list pl-5">
                    <li v-for="answer of answers.filter(a => a.questionId === question.id)" v-bind:key="answer" class="mb-3">
                        <i v-bind:class="bulletStyle(quiz, answer)" class="fa mr-3 float-left"></i>
                        <h6 class="text-left"><router-link :to="'/answer/edit/' + answer.id" >{{ answer.answerText }}</router-link>
<!--                            <span v-if="quiz.quizOrPoll === 'Q'" class="badge badge-success badge-pill float-right">Chosen by: {{ answer.answerers.slice(0, 5).join(", ") }}</span>-->
                            <span class="badge badge-primary badge-pill float-right" >Chosen by: {{ answer.answerers.length }}</span>
                            <a @click="deleteAnswer(answer.id)" class="fa mr-3 text-primary fa-times text-danger justify-content-end ml-3"> <span class="ml-1">Delete</span></a>
                        </h6>
                        <img class="float-left" v-if="answer.url" v-bind:src="answer.url" width="100" height="100">
                    </li>
                </ul>
                <hr class="border-dark">
                <div v-if="quiz.quizOrPoll === 'Q'">
                    <p class="mt-4 mb-n3">
                        <button class="btn btn-primary" type="button" data-toggle="collapse" :data-target="'#' + question.id" aria-expanded="false" aria-controls="collapseExample">
                            All votes
                        </button>
                    </p>
                    <div class="collapse mt-4" :id="question.id">
                        <div v-for="answer in answers.filter(a => a.questionId === question.id)" v-bind:key="answer" class="card card-body mb-3">
                             <h4>{{ answer.answerText }}</h4>
                            <hr class="border-dark">
                            <div v-for="answerer in answer.answerers" v-bind:key="answerer">
                                <h5>
                                    {{ answerer }}
                                </h5>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <button @click="modalAnswerCurrent !== question.id ? modalAnswerCurrent = question.id : modalAnswerCurrent = ''" type="button"
                    class="btn btn-outline-secondary btn-sm mb-3 text-center w-25 align-self-center">Add new answer</button>
            <answer-create v-show="modalAnswerCurrent === question.id" :set-question-id="question.id" :return="true"></answer-create>
        </div>
    </div>
    <div v-else>
        <div class="spinner-border text-primary" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store'
import { BaseService } from '@/services/base-service'
import IQuiz from '@/domain/IQuiz'
import QuizEdit from '@/views/quiz/Edit.vue'
import IQuestion from '@/domain/IQuestion'
import IAnswer from '@/domain/IAnswer'
import AnswerCreate from '@/views/answer/Create.vue'
import QuestionCreate from '@/views/question/Create.vue'
@Options({
    components: {
        QuizEdit,
        AnswerCreate,
        QuestionCreate
    },
    props: {
        id: String,
    }
})
export default class QuizView extends Vue {
    id!: string;
    loading: boolean = true;
    private modal: boolean = false;
    private questionModal: boolean = false;
    private modalAnswerCurrent: string = "";

    quiz!: IQuiz;
    questions: IQuestion[] = [];
    answers: IAnswer[] = [];

    bulletStyle(quiz: IQuiz, answer: IAnswer): string {
        if (quiz.quizOrPoll !== 'Q') return 'fa-circle text-primary fa-xs';
        if (answer.isCorrect) return 'fa-check text-success';
        else return 'fa-times text-danger';
    }

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quiz = data.data!;
            }
        });
        console.log(this.role !== 'Admin' && this.quiz.appUserId !== this.userId)
        if (this.role !== 'Admin' && this.quiz.appUserId !== this.userId) await this.$router.push('/');
        const questionService = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await questionService.getAll({ quizId: this.quiz.id }).then((data) => {
            if (data.ok) {
                this.questions = data.data!;
            }
        });
        const answerService = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await answerService.getAll({ quizId: this.quiz.id }).then((data) => {
            if (data.ok) {
                this.answers = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteQuestion(id: string): Promise<void> {
        const service = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await service.delete(id).then((data) => {
            if (data.ok) {
                this.$router.go(0);
            } else {
                console.log(data)
            }
        });
    }

    async deleteAnswer(id: string): Promise<void> {
        const service = new BaseService<IQuestion>('v1/answers', this.token || undefined);
        await service.delete(id).then((data) => {
            if (data.ok) {
                this.$router.go(0);
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>
</style>
