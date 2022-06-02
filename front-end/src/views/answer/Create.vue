<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Add answer</h5>
                            <div class="form-signin">
                                <div v-show="!setQuestionId" class="form-group">
                                    <label class="control-label" for="questionId">Question</label>
                                    <select v-model="questionId" class="form-control" id="questionId" name="questionId">
                                        <option v-for="question of questions" v-bind:key="question.id" v-bind:value="question.id">
                                            {{ question.questionText }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="answerText" type="text" id="inputAnswerText" class="form-control" required placeholder="Answer">
                                    <label for="inputAnswerText">Answer</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="url" type="text" id="inputUrl" class="form-control" required placeholder="Picture (optional)">
                                    <label for="inputUrl">Picture (optional)</label>
                                </div>
                                <div class="custom-control custom-checkbox mb-3">
                                    <input :disabled="questions.find(q => q.id === questionId)?.hasCorrectAnswer" v-model="isCorrect" type="checkbox" class="custom-control-input" id="customCheck1">
                                    <label class="custom-control-label" for="customCheck1">Correct</label>
                                </div>
                                <button @click="createAnswer" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div v-if="!returnLink">
                                <router-link class="mx-4" to="/answers">Back to List</router-link>
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
import IAnswerAdd from '@/domain/IAnswerAdd'
import IQuestion from '@/domain/IQuestion'
@Options({
    components: {},
    props: {
        setQuestionId: String,
        return: Boolean,
    }
})
export default class AnswerCreate extends Vue {
    setQuestionId?: string;
    return?: boolean;

    questionId!: string;
    isCorrect: boolean = false;
    answerText!: string;
    url: string | undefined;

    private loading: boolean = true;

    questions: IQuestion[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        if (store.state.id === undefined) await this.$router.push('/login');
        if (this.setQuestionId) this.questionId = this.setQuestionId;
        else {
            const service = new BaseService<IQuestion>('v1/questions', this.token || undefined);
            await service.getAll().then((data) => {
                if (data.ok) {
                    this.questions = data.data!;
                } else {
                    console.log(data)
                }
            });
        }
        this.loading = false;
    }

    async createAnswer(): Promise<void> {
        const answer: IAnswerAdd = {
            questionId: this.questionId!,
            isCorrect: this.questions.find(q => q.id === this.questionId)?.hasCorrectAnswer ? false : this.isCorrect,
            answerText: this.answerText,
            url: this.url,
        };
        const service = new BaseService<IAnswerAdd>('v1/answers', this.token || undefined);
        await service.post(answer).then((data) => {
            if (data.ok) {
                if (this.return) {
                    this.$router.go(0);
                } else this.$router.push('/answers');
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
