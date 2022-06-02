<template>
    <body>
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Create quiz</h5>
                            <div class="form-signin">
                                <div class="form-label-group">
                                    <input v-model="title" type="text" id="inputName" class="form-control" placeholder="Title" required>
                                    <label for="inputName">Title</label>
                                </div>
                                <div>
                                    <input name="qOrP" type="radio" id="poll" v-bind:value="'P'" v-model="quizOrPoll" required>
                                    <label for="poll">Poll</label>
                                    <br>
                                    <input name="qOrP" type="radio" id="quiz" v-bind:value="'Q'" v-model="quizOrPoll" required>
                                    <label for="quiz">Quiz</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="timeLimit" type="number" id="inputTimeLimit" class="form-control" placeholder="Time limit in minutes (optional)" required>
                                    <label for="inputTimeLimit">Time limit in minutes (optional)</label>
                                </div>
                                <div class="custom-control custom-checkbox mb-3">
                                    <input v-model="repeatable" type="checkbox" class="custom-control-input" id="customCheck1">
                                    <label class="custom-control-label" for="customCheck1">Is test repeatable?</label>
                                </div>
                                <div class="custom-control custom-checkbox mb-3">
                                    <input v-model="showAnswers" type="checkbox" class="custom-control-input" id="customCheck2">
                                    <label class="custom-control-label" for="customCheck2">Show answers after submission?</label>
                                </div>
                                <button @click="createQuiz" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
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
import IQuizAdd from '@/domain/IQuizAdd'

export default class QuizCreate extends Vue {
    appUserId!: string;
    title!: string;
    quizOrPoll!: string;
    createdAt!: string;
    timeLimit: number | undefined;
    repeatable: boolean = true;
    showAnswers: boolean = false;

    get id(): string | undefined {
        return store.state.id;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (!this.id) await this.$router.push('/login');
    }

    async createQuiz(): Promise<void> {
        const quiz: IQuizAdd = { appUserId: store.state.id!, title: this.title, quizOrPoll: this.quizOrPoll, timeLimit: this.timeLimit, repeatable: this.repeatable, showAnswers: this.showAnswers };
        const service = new BaseService<IQuizAdd>('v1/quizzes', this.token || undefined);
        await service.post(quiz).then((data) => {
            if (data.ok) {
                this.$router.push('/quiz/' + data.data)
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
