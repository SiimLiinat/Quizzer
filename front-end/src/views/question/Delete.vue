<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Question</h4>
            <hr />
            <dl class="row">
                <dt class = "col-sm-2">
                    Quiz
                </dt>
                <dd class = "col-sm-10">
                    {{question.quizTitle}}
                </dd>
                <dt class = "col-sm-2">
                    Question
                </dt>
                <dd class = "col-sm-10">
                    {{question.questionText}}
                </dd>
                <dt class = "col-sm-2">
                    Points
                </dt>
                <dd class = "col-sm-10">
                    {{question.points}}
                </dd>
                <dt class = "col-sm-2">
                    Order
                </dt>
                <dd class = "col-sm-10">
                    {{question.questionNumber}}
                </dd>
                <dt class = "col-sm-2">
                    Picture
                </dt>
                <dd class = "col-sm-10">
                    <img v-bind:src="question.url" width="400" height="400">
                </dd>
            </dl>
            <div>
                <input @click="deleteQuestion" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/question/edit/' + question.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/question/details/' + question.id">Details</router-link>
            <span> | </span>
            <router-link to="/questions">Back to List</router-link>
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
import IQuestion from '@/domain/IQuestion'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class QuestionDelete extends Vue {
    id!: string;
    private question!: IQuestion;
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
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.question = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteQuestion(): Promise<void> {
        const service = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/questions')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
