<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Quiz</h4>
            <hr />
            <dl class="row">
                <dt class = "col-sm-2">
                    Author
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.appUserName}}
                </dd>
                <dt class = "col-sm-2">
                    Title
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.title}}
                </dd>
                <dt class = "col-sm-2">
                    Quiz or Poll
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.quizOrPoll}}
                </dd>
                <dt class = "col-sm-2">
                    Time Limit
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.timeLimit}}
                </dd>
                <dt class = "col-sm-2">
                    Created at
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.createdAt}}
                </dd>
                <dt class = "col-sm-2">
                    Repeatable
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.repeatable}}
                </dd>
                <dt class = "col-sm-2">
                    Show answers
                </dt>
                <dd class = "col-sm-10">
                    {{quiz.showAnswers}}
                </dd>
            </dl>
            <div>
                <input @click="deleteQuiz" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/quiz/edit/' + quiz.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/quiz/details/' + quiz.id">Details</router-link>
            <span> | </span>
            <router-link to="/quizzes">Back to List</router-link>
        </div>
        <div v-else>
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IQuiz from '@/domain/IQuiz'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class QuizDelete extends Vue {
    id!: string;
    private quiz!: IQuiz;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quiz = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteQuiz(): Promise<void> {
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/quizzes')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
