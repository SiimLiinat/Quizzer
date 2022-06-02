<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create question</h5>
                            <div class="form-signin">
                                <div v-show="!setQuizId" class="form-group">
                                    <label class="control-label" for="devCompanyId">Quiz</label>
                                    <select required v-model="quizId" class="form-control" id="devCompanyId" name="companyId">
                                        <option v-for="quiz of quizzes" v-bind:key="quiz.id" v-bind:value="quiz.id">
                                            {{ quiz.title }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="questionText" type="text" id="inputQuestion" class="form-control" placeholder="Question" required>
                                    <label for="inputQuestion">Question</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="url" type="text" id="inputUrl" class="form-control" placeholder="Picture (optional)">
                                    <label for="inputUrl">Picture (optional)</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="points" type="number" id="inputPoints" class="form-control" placeholder="Points (optional)">
                                    <label for="inputPoints">Points (optional)</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="questionNumber" type="number" id="inputQuestionNumber" class="form-control" placeholder="Question's position (optional)">
                                    <label for="inputQuestionNumber">Question's position (optional)</label>
                                </div>
                                <button @click="createQuestion" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div v-show="!setQuizId">
                                <router-link class="mx-4" to="/questions">Back to List</router-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
    <div v-else>
        <div class="spinner-border text-light" role="status">
            <span class="sr-only">Loading...</span>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IQuiz from '@/domain/IQuiz'
import IQuestionAdd from '@/domain/IQuestionAdd'
@Options({
    components: {},
    props: {
        setQuizId: String,
        return: Boolean,
    }
})
export default class QuestionCreate extends Vue {
    setQuizId?: string;
    return?: boolean;

    quizId!: string;
    questionText!: string;
    url: string | undefined;
    points: number | undefined;
    questionNumber: number | undefined;

    private loading: boolean = true;

    quizzes: IQuiz[] = [];

    get userId(): string | undefined {
        return store.state.id;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (!this.userId) await this.$router.push('/');
        if (this.setQuizId) this.quizId = this.setQuizId;
        const quizService = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        quizService.getAll().then((data) => {
            if (data.ok) {
                this.quizzes = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async createQuestion(): Promise<void> {
        const question: IQuestionAdd = { quizId: this.quizId, questionText: this.questionText, questionNumber: this.questionNumber, points: this.points || 1, url: this.url };
        const service = new BaseService<IQuestionAdd>('v1/questions', this.token || undefined);
        await service.post(question).then((data) => {
            if (data.ok) {
                if (this.return) this.$router.go(0);
                else this.$router.push('/questions')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
