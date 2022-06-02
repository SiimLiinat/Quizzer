<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit quiz</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="quiz.title" type="text" id="inputName" class="form-control" placeholder="Title" required>
                                <label for="inputName">Title</label>
                            </div>
                            <div>
                                <input type="radio" id="quiz" value="Q" v-model="quiz.quizOrPoll">
                                <label for="quiz">Quiz</label>
                                <br>
                                <input type="radio" id="poll" value="P" v-model="quiz.quizOrPoll">
                                <label for="poll">Poll</label>
                                <br>
                            </div>
                            <div class="form-label-group">
                                <input v-model="quiz.timeLimit" type="number" id="inputTimeLimit" class="form-control" placeholder="Time limit - minutes (optional)" required>
                                <label for="inputTimeLimit">Time limit - minutes (optional)</label>
                            </div>
                            <div class="custom-control custom-checkbox mb-3">
                                <input v-model="quiz.repeatable" type="checkbox" class="custom-control-input" id="customCheck1">
                                <label class="custom-control-label" for="customCheck1">Is test repeatable?</label>
                            </div>
                            <div class="custom-control custom-checkbox mb-3">
                                <input v-model="quiz.showAnswers" type="checkbox" class="custom-control-input" id="customCheck2">
                                <label class="custom-control-label" for="customCheck2">Show answers after submission?</label>
                            </div>
                            <button @click="editQuiz" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Edit</button>
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
import IQuiz from '@/domain/IQuiz';
@Options({
    components: {},
    props: {
        id: String,
        refreshPage: Boolean
    }
})
export default class QuizEdit extends Vue {
    id!: string;
    refreshPage?: boolean;
    private loading: boolean = true;

    quiz!: IQuiz;

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    get role(): string | undefined {
        return store.state.role;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quiz = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        if (this.role !== 'Admin' && this.quiz.appUserId !== this.userId) await this.$router.push('/');
        this.loading = false;
    }

    async editQuiz(): Promise<void> {
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.put(this.id, this.quiz).then((data) => {
            if (data.ok) {
                if (this.refreshPage) this.$router.go(0);
                this.$router.push('/quizzes')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
