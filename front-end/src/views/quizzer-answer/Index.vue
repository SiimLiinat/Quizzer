<template>
    <div class="card card-body mt-5">
        <h1>Quizzer Answers</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/quizzer-answer/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Quiz
                    </th>
                    <th>
                        Answer
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="quizzerAnswer of quizzerAnswers" v-bind:key="quizzerAnswer">
                    <td>
                        {{quizzerAnswer.quizzerId}}
                    </td>
                    <td>
                        {{quizzerAnswer.answerId}}
                    </td>
                    <td>
                        <router-link :to="'/quizzer-answer/details/' + quizzerAnswer.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/quizzer-answer/delete/' + quizzerAnswer.id">Delete</router-link>
                    </td>
                </tr>
                </tbody>
            </table>
        </div>
        <div v-else>
            <div class="spinner-border text-primary" role="status">
                <span class="sr-only">Loading...</span>
            </div>
        </div>
    </div>
</template>

<script lang="ts">
import { Vue } from 'vue-class-component';
import store from '@/store';
import { BaseService } from '@/services/base-service';
import IQuizzerAnswer from '@/domain/IQuizzerAnswer'

export default class QuizzerAnswerIndex extends Vue {
    private quizzerAnswers: IQuizzerAnswer[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IQuizzerAnswer>('v1/quizzerAnswers', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.quizzerAnswers = data.data!;
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
