<template>
    <div v-if="!loading" class="container">
        <div class="main-body">
            <div class="row gutters-sm">
                <div class="col-md-4 mb-3">
                    <div class="card">
                        <div class="card-body">
                            <div v-once class="d-flex flex-column align-items-center text-center">
                                <img v-bind:src="user.profilePicture !== null ? user.profilePicture
                                : require('../../../public/default_profile_pic.webp')" alt="" class="rounded-circle" width="150">
                                <div class="mt-3">
                                    <h4>{{ user.userName }}</h4>
                                    <button class="btn btn-outline-primary">Message</button>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="card mt-3">
                        <ul class="list-group list-group-flush">
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0 font-weight-bold">Quizzes created</h6>
                                <span class="text-dark">{{ quizzes.length }}</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0 font-weight-bold">Quizzes taken</h6>
                                <span class="text-dark">{{ quizzers.length }}</span>
                            </li>
                            <li class="list-group-item d-flex justify-content-between align-items-center flex-wrap">
                                <h6 class="mb-0 font-weight-bold">Average score</h6>
                                <span :class="getColor(averageScore(quizzers))">{{ averageScore(quizzers) }} %</span>
                            </li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-8">
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="row">
                                <div class="col-sm-3">
                                    <h6 class="mb-0">User Name</h6>
                                </div>
                                <div class="col-sm-3 text-primary float-left">
                                    {{ user.userName }}
                                </div>
                            </div>
                            <hr>
                            <div v-if="userId === id" class="row mb-3">
                                <div class="col-sm-3">
                                    <h6 class="mb-0 align-items-center my-2">Email</h6>
                                </div>
                                <div class="col-sm-9 text-primary">
                                    <input v-on:change="emailChange = true" v-model="user.email" type="text" class="form-control">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-3">
                                    <h6 class="mb-0 align-items-center my-2">Profile Picture</h6>
                                </div>
                                <div class="col-sm-9 text-primary">
                                    <input :disabled="userId !== id"  v-on:change="profilePictureChange = true" v-model="user.profilePicture" type="text" class="form-control">
                                </div>
                            </div>
                            <hr>
                            <button v-if="userId === id" @click="editProfile" :disabled="!emailChange && !profilePictureChange"
                                    class="btn btn-primary">Save changes</button>
                        </div>
                    </div>

                    <div class="row gutters-sm">
                        <div class="col-sm-6 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="align-items-center mb-3">Quizzes created</h4>
                                    <div v-for="quiz of quizzes.slice(0, 10)" v-bind:key="quiz">
                                        <router-link :to="userId === id ? '/quiz/' + quiz.id : '/quiz/take/' + quiz.id">{{ quiz.title }}</router-link>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <h4 class="align-items-center mb-3">Quizzes completed</h4>
                                    <div v-for="quizzer of quizzers.filter(q => q.finishedAt).slice(0, 10)" v-bind:key="quizzer">
                                        <router-link :to="'/quiz/results/' + quizzer.id">{{ quizzer.quizTitle }}</router-link>
                                    </div>
                                    <hr>
                                    <router-link role="button" class="btn btn-primary" v-if="id === userId" to="/quizzers">See all attempts</router-link>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component'
import { BaseService } from '@/services/base-service'
import IAppUser from '@/domain/IAppUser'
import store from '@/store'
import IQuiz from '@/domain/IQuiz'
import IQuizzer from '@/domain/IQuizzer'

@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class Overview extends Vue {
    id!: string;
    private loading: boolean = true;
    private user!: IAppUser;
    private emailChange: boolean = false;
    private profilePictureChange: boolean = false;

    private quizzes: IQuiz[] = [];
    private quizzers: IQuizzer[] = [];

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    averageScore(quizzers: IQuizzer[]): number {
        const percentages = quizzers.filter(q => q.finishedAt).map(q => q.quizPercentage);
        const sum = percentages.reduce((a, b) => a + b, 0);
        return (sum / percentages.length) || 0;
    }

    async mounted(): Promise<void> {
        const userService = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await userService.get(this.id!).then((data) => {
            if (data.ok) {
                this.user = data.data!;
            } else {
                this.$router.push({ name: 'Login' })
            }
        });
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.getAll({ userId: this.id }).then((data) => {
            if (data.ok) {
                this.quizzes = data.data!;
            } else {
                console.log(data)
            }
        });
        const quizzerService = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await quizzerService.getAll({ userId: this.id }).then((data) => {
            if (data.ok) {
                this.quizzers = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }

    getColor(score: number): string {
        if (score >= 80) {
            return 'text-primary'
        } else if (score >= 60) {
            return 'text-success'
        } else if (score >= 40) {
            return 'text-warning'
        } else if (score >= 20) {
            return 'text-danger'
        } else return 'dark';
    }

    async editProfile(): Promise<void> {
        const service = new BaseService<IAppUser>('v1/appUsers', this.token || undefined);
        await service.put(this.id!, this.user).then((data) => {
            if (data.ok) {
                this.$forceUpdate();
                this.$router.push('/profile/' + this.id);
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../../../node_modules/mdb-ui-kit/css/mdb.min.css">

body{
    margin-top:20px;
    color: #1a202c;
    text-align: left;
    background-color: #e2e8f0;
}
.main-body {
    padding: 15px;
}
.card {
    box-shadow: 0 1px 3px 0 rgba(0,0,0,.1), 0 1px 2px 0 rgba(0,0,0,.06);
}

.card {
    position: relative;
    display: flex;
    flex-direction: column;
    min-width: 0;
    word-wrap: break-word;
    background-color: #fff;
    background-clip: border-box;
    border: 0 solid rgba(0,0,0,.125);
    border-radius: .25rem;
}

.card-body {
    flex: 1 1 auto;
    min-height: 1px;
    padding: 1rem;
}

.gutters-sm {
    margin-right: -8px;
    margin-left: -8px;
}

.gutters-sm, .gutters-sm {
    padding-right: 8px;
    padding-left: 8px;
}
.mb-3 {
    margin-bottom: 1rem!important;
}

</style>
