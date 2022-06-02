<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create quizzer's answer</h5>
                            <div class="form-signin">
                                <div class="form-group">
                                    <label class="control-label" for="quizzerId">Quizzer</label>
                                    <select required v-model="quizzerId" class="form-control" id="quizzerId" name="quizzerId">
                                        <option v-for="quizzer of quizzers" v-bind:key="quizzer.id" v-bind:value="quizzer.id">
                                            {{ quizzer.appUserName }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="answerId">Answer</label>
                                    <select required v-model="answerId" class="form-control" id="answerId" name="answerId">
                                        <option v-for="answer of answers" v-bind:key="answer.id" v-bind:value="answer.id">
                                            {{ answer.answerText }}
                                        </option>
                                    </select>
                                </div>
                                <button @click="createQuizzerAnswer" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/quizzes">Back to List</router-link>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </body>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IQuizzerAnswerAdd from '@/domain/IQuizzerAnswerAdd';
import IQuizzer from '@/domain/IQuizzer';
import IAnswer from '@/domain/IAnswer';

export default class QuizzerAnswerCreate extends Vue {
    answerId!: string;
    quizzerId!: string;

    quizzers: IQuizzer[] = [];
    answers: IAnswer[] = [];
    loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const quizzerService = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await quizzerService.getAll().then((data) => {
            if (data.ok) {
                this.quizzers = data.data!;
            } else {
                console.log(data)
            }
        });
        const userService = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await userService.getAll().then((data) => {
            if (data.ok) {
                this.answers = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async createQuizzerAnswer(): Promise<void> {
        const quizzerAnswer: IQuizzerAnswerAdd = { answerId: this.answerId, quizzerId: this.quizzerId };
        const service = new BaseService<IQuizzerAnswerAdd>('v1/quizzerAnswers', this.token || undefined);
        await service.post(quizzerAnswer).then((data) => {
            if (data.ok) {
                this.$router.push('/quizzer-answers')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
