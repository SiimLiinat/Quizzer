<template>
    <div class="card card-body mt-5">
        <h1>Quizzes</h1>
        <p>
            <router-link to="/quiz/create">Create New</router-link>
        </p>
        <div class="row">
            <div class="col-md-6 col-lg-3 my-3">
                <div class="input-group position-relative">
                    <input @input="filterData" v-model="searchWords" type="text" class="form-control" placeholder="Enter Your Keywords"
                           id="keywords">
                </div>
            </div>
            <div class="col-md-6 col-lg-3 my-3">
                <div class="select-container">
                    <select @change="filterData" v-model="repeatable" class="form-control custom-select" id="repeatableId"
                            name="repeatableId">
                        <option v-bind:value="undefined" selected>Select repeatability</option>
                        <option v-bind:value="true">Yes</option>
                        <option v-bind:value="false">No</option>
                    </select>
                </div>
            </div>
            <div class="col-md-6 col-lg-3 my-3">
                <div class="select-container">
                    <select @change="filterData" v-model="timeLimit" class="form-control custom-select" id="timeLimitId" name="timeLimitId">
                        <option v-bind:value="undefined" selected>Select time limit</option>
                        <option v-bind:value="true">Yes</option>
                        <option v-bind:value="false">No</option>
                    </select>
                </div>
            </div>
            <div class="col-md-6 col-lg-3 my-3">
                <div class="select-container">
                    <select @change="filterData" v-model="quizType" class="form-control custom-select" id="quizTypeId" name="quizTypeId">
                        <option v-bind:value="undefined" selected>Select quiz type</option>
                        <option v-bind:value="'Q'">Quiz</option>
                        <option v-bind:value="'P'">Poll</option>
                    </select>
                </div>
            </div>
        </div>
        <div v-if="!loading">
            <table class="table table-hover table-responsive-sm .table-responsive{-sm|-md|-lg|-xl}">
                <thead>
                <tr>
                    <th @click="sortData('author')">
                        Author<i class="fas fa-sort"></i>
                    </th>
                    <th @click="sortData('title')">
                        Title<i class="fas fa-sort"></i>
                    </th>
                    <th @click="sortData('type')">
                        Type<i class="fas fa-sort"></i>
                    </th>
                    <th @click="sortData('timeLimit')">
                        Time Limit<i class="fas fa-sort"></i>
                    </th>
                    <th @click="sortData('createdAt')">
                        Created At<i class="fas fa-sort"></i>
                    </th>
                    <th @click="sortData('repeatable')">
                        Repeatable<i class="fas fa-sort"></i>
                    </th>
                    <th/>
                </tr>
                </thead>
                <tbody>
                <tr v-for="quiz of filteredData" v-bind:key="quiz">
                    <td>
                        {{quiz.appUserName}}
                    </td>
                    <td>
                        {{quiz.title}}
                    </td>
                    <td>
                        {{quiz.quizOrPoll === 'Q' ? 'Quiz' : 'Poll'}}
                    </td>
                    <td>
                        <div v-if="quiz.timeLimit">
                            {{quiz.timeLimit}} min
                        </div>
                        <div v-else>
                            <i class="fa fa-minus text-danger"></i>
                        </div>
                    </td>
                    <td>
                        {{quiz.createdAt}}
                    </td>
                    <td>
                        <i v-if="quiz.repeatable" class="fa fa-check text-success"></i>
                        <i v-else class="fa fa-times text-danger"></i>
                    </td>
                    <td>
                        <div v-if="role === 'Admin'">
                            <router-link :to="'/quiz/edit/' + quiz.id">Edit</router-link>
                            <span> | </span>
                            <router-link :to="'/quiz/details/' + quiz.id">Details</router-link>
                            <span> | </span>
                            <router-link :to="'/quiz/delete/' + quiz.id">Delete</router-link>
                            <span> | </span>
                        </div>
                        <div v-if="role === 'Admin' || quiz.appUserId === id">
                            <router-link :to="id ? '/quiz/' + quiz.id : '/login'">Quiz view</router-link>
                            <span> | </span>
                        </div>
                        <div v-if="role === 'Admin' || quiz.appUserId === id || quiz.repeatable
                            || !quizzers.find(q => q.quizId === q.quizId)" >
                            <router-link v-if="role === 'Admin' || quiz.appUserId === id || quiz.repeatable
                            || !quizzers.find(q => q.quizId === q.quizId)" :to="id ? '/quiz/take/' + quiz.id : '/login'">Take quiz</router-link>
                        </div>
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
import IQuiz from '@/domain/IQuiz';
import IQuizzer from '@/domain/IQuizzer'

export default class QuizIndex extends Vue {
    private quizzes: IQuiz[] = [];
    private quizzers: IQuizzer[] = [];
    private loading: boolean = true;

    private filteredData: IQuiz[] = [];

    get role(): string | undefined {
        return store.state.role;
    }

    get id(): string | undefined {
        return store.state.id;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    private searchWords: string | undefined;
    private repeatable: boolean | undefined = undefined;
    private timeLimit: boolean | undefined = undefined;
    private quizType: string | undefined = undefined;

    filterData(): void {
        this.filteredData = this.quizzes;
        if (this.searchWords !== undefined && this.searchWords !== "") {
            const searchWords = this.searchWords.toUpperCase().split(" ");
            this.filteredData = this.filteredData.filter(g => this.ContainsAny(g.title.toUpperCase(), searchWords));
        }
        if (this.repeatable !== undefined) this.filteredData = this.filteredData.filter(g => g.repeatable === this.repeatable);
        if (this.timeLimit !== undefined) this.filteredData = this.filteredData.filter(g => (g.timeLimit === undefined) === this.timeLimit);
        if (this.quizType !== undefined) this.filteredData = this.filteredData.filter(g => g.quizOrPoll === this.quizType);
    }

    ContainsAny(str: string, items: { [x: string]: any }): boolean {
        for (const i in items) {
            const item = items[i];
            if (str.indexOf(item) > -1) {
                return true;
            }
        }
        return false;
    }

    sortData(field: string): void {
        if (field === 'title') {
            if (!this.sorted(this.filteredData, field)) this.filteredData = this.filteredData.sort((a, b) => a.title > b.title ? 1 : -1)
            else this.filteredData = this.filteredData.sort((a, b) => a.title > b.title ? -1 : 1)
        } else if (field === 'repeatable') {
            if (!this.sorted(this.filteredData, field)) this.filteredData = this.filteredData.sort((a, b) => a.repeatable && !b.repeatable ? -1 : 1)
            else this.filteredData = this.filteredData.sort((a, b) => a.repeatable && !b.repeatable ? 1 : -1)
        } else if (field === 'timeLimit') {
            if (!this.sorted(this.filteredData, field)) this.filteredData = this.filteredData.sort((a, b) => a.timeLimit && !b.timeLimit ? -1 : 1)
            else this.filteredData = this.filteredData.sort((a, b) => a.timeLimit && !b.timeLimit ? 1 : -1)
        } else if (field === 'type') {
            if (!this.sorted(this.filteredData, field)) this.filteredData = this.filteredData.sort((a, b) => a.quizOrPoll > b.quizOrPoll ? -1 : 1)
            else this.filteredData = this.filteredData.sort((a, b) => a.quizOrPoll > b.quizOrPoll ? 1 : -1)
        } else if (field === 'author') {
            if (!this.sorted(this.filteredData, field)) this.filteredData = this.filteredData.sort((a, b) => a.appUserName > b.appUserName ? -1 : 1)
            else this.filteredData = this.filteredData.sort((a, b) => a.appUserName > b.appUserName ? 1 : -1)
        } else if (field === 'createdAt') {
            if (!this.sorted(this.filteredData, field)) this.filteredData = this.filteredData.sort((a, b) => a.createdAt > b.createdAt ? -1 : 1)
            else this.filteredData = this.filteredData.sort((a, b) => a.createdAt > b.createdAt ? 1 : -1)
        }
    }

    sorted(array: IQuiz[], field: string): boolean {
        let sorted: boolean = true;

        for (let i = 0; i < array.length - 1; i++) {
            if (field === 'title' && array[i].title > array[i + 1].title) {
                sorted = false;
                break;
            }
            if (field === 'repeatable' && !array[i].repeatable && array[i + 1].repeatable) {
                sorted = false;
                break;
            }
            if (field === 'timeLimit' && !array[i].timeLimit && array[i + 1].timeLimit) {
                sorted = false;
                break;
            }
            if (field === 'type' && array[i].quizOrPoll < array[i + 1].quizOrPoll) {
                sorted = false;
                break;
            }
            if (field === 'author' && array[i].appUserName < array[i + 1].appUserName) {
                sorted = false;
                break;
            }
            if (field === 'createdAt' && array[i].createdAt < array[i + 1].createdAt) {
                sorted = false;
                break;
            }
        }
        return sorted;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IQuiz>('v1/quizzes', this.token || undefined);
        await service.getAll().then((data) => {
            if (data.ok) {
                this.quizzes = data.data!;
                this.filteredData = this.quizzes;
            } else {
                console.log(data)
            }
        });
        const quizzerService = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await quizzerService.getAll({ userId: this.id }).then((data) => {
            if (data.ok) {
                this.quizzers = data.data!.filter(q => q.quizId === this.id);
            } else {
                console.log(data)
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>
    th {
        cursor: pointer
    }
</style>
