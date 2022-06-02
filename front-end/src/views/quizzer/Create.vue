<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create quizzer</h5>
                            <div class="form-signin">
                                <div class="form-group">
                                    <label class="control-label" for="userId">User</label>
                                    <select required v-model="appUserId" class="form-control" id="userId" name="companyId">
                                        <option v-for="user of users" v-bind:key="user.id" v-bind:value="user.id">
                                            {{ user.userName }}
                                        </option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <label class="control-label" for="quizId">Quiz</label>
                                    <select required v-model="quizId" class="form-control" id="quizId" name="quizId">
                                        <option v-for="quiz of quizzes" v-bind:key="quiz.id" v-bind:value="quiz.id">
                                            {{ quiz.title }}
                                        </option>
                                    </select>
                                </div>
                                <button @click="createQuizzer" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/quizzers">Back to List</router-link>
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
import IQuizzerAdd from '@/domain/IQuizzerAdd';
import IQuiz from '@/domain/IQuiz'
import IAppUser from '@/domain/IAppUser'

export default class QuizzerCreate extends Vue {
    appUserId!: string;
    quizId!: string;

    quizzes: IQuiz[] = [];
    users: IAppUser[] = [];
    loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const quizService = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await quizService.getAll().then((data) => {
            if (data.ok) {
                this.quizzes = data.data!;
            } else {
                console.log(data)
            }
        });
        const userService = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await userService.getAll().then((data) => {
            if (data.ok) {
                this.users = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    async createQuizzer(): Promise<void> {
        const quizzer: IQuizzerAdd = { appUserId: this.appUserId, quizId: this.quizId };
        const service = new BaseService<IQuizzerAdd>('v1/quizzers', this.token || undefined);
        await service.post(quizzer).then((data) => {
            if (data.ok) {
                this.$router.push('/quizzers')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
