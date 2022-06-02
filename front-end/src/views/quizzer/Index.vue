<template>
    <div class="card card-body mt-5">
        <h1>Attempts</h1>
        <p>
            <router-link v-if="role === 'Admin'" to="/quizzer/create">Create New</router-link>
        </p>
        <div v-if="!loading">
            <table class="table .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th>
                        User
                    </th>
                    <th>
                        Quiz Title
                    </th>
                    <th>
                        Started at
                    </th>
                    <th>
                        Finished at
                    </th>
                    <th>
                        Points
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="quizzer of quizzers" v-bind:key="quizzer">
                    <td>
                        {{quizzer.appUserName}}
                    </td>
                    <td>
                        {{quizzer.quizTitle}}
                    </td>
                    <td>
                        {{quizzer.startedAt.split("T").join(" ")}}
                    </td>
                    <td>
                        {{quizzer.finishedAt?.split("T").join(" ")}}
                    </td>
                    <td>
                        {{quizzer.points}}
                    </td>
                    <td v-if="role === 'Admin'">
                        <router-link :to="'/quizzer/details/' + quizzer.id">Details</router-link>
                        <span> | </span>
                        <router-link :to="'/quizzer/delete/' + quizzer.id">Delete</router-link>
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
import IQuizzer from '@/domain/IQuizzer';

export default class QuizzerIndex extends Vue {
    private quizzers: IQuizzer[] = [];
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (!this.id) await this.$router.push('/');
        const service = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await service.getAll({ userId: this.role === 'Admin' ? null : this.id }).then((data) => {
            if (data.ok) {
                this.quizzers = data.data!;
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
