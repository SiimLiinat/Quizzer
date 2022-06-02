<template>
    <div class="card card-body mt-5">
        <h1>Questions</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/question/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Quiz
                    </th>
                    <th>
                        Question
                    </th>
                    <th>
                        Points
                    </th>
                    <th>
                        Order
                    </th>
                    <th>
                        Picture
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="question of questions" v-bind:key="question">
                    <td>
                        {{question.quizTitle}}
                    </td>
                    <td>
                        {{question.questionText}}
                    </td>
                    <td>
                        {{question.points}}
                    </td>
                    <td>
                        {{question.questionNumber}}
                    </td>
                    <td>
                        <img v-bind:src="question.url" height="100">
                    </td>
                    <td>
                        <router-link :to="'/question/edit/' + question.id">Edit</router-link>
                        <span> | </span>
                        <router-link :to="'/question/details/' + question.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/question/delete/' + question.id">Delete</router-link>
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
import IQuestion from '@/domain/IQuestion';

export default class QuestionIndex extends Vue {
    private questions: IQuestion[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.questions = data.data!;
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
