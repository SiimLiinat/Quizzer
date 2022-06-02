<template>
    <div class="card card-body mt-5">
        <h1>Answers</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/answer/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        Question
                    </th>
                    <th>
                        Answer
                    </th>
                    <th>
                        Correct
                    </th>
                    <th>
                        Url
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="answer of answers" v-bind:key="answer">
                    <td>
                        {{answer.question}}
                    </td>
                    <td>
                        {{answer.answerText}}
                    </td>
                    <td>
                        {{answer.isCorrect}}
                    </td>
                    <td>
                        <img v-if="answer.url" v-bind:src="answer.url" height="100">
                    </td>
                    <td>
                        <router-link v-if="id" :to="'/answer/edit/' + answer.id">Edit</router-link>
                        <span v-if="id"> | </span>
                        <router-link :to="'/answer/details/' + answer.id">Details</router-link>
                        <span v-if="id"> | </span>
                        <router-link v-if="id" :to="'/answer/delete/' + answer.id">Delete</router-link>
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
import IAnswer from '@/domain/IAnswer'

export default class AnswerIndex extends Vue {
    private answers: IAnswer[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.answers = data.data!;
            } else {
                console.log(data)
            }
        });
        if (this.id === undefined) await this.$router.push('/');
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
