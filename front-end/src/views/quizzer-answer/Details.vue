<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Quiz</h4>
            <hr />
            <dl class="row">
                <dt class = "col-sm-2">
                    Quizzer
                </dt>
                <dd class = "col-sm-10">
                    {{quizzerAnswer.quizzerId}}
                </dd>
                <dt class = "col-sm-2">
                    Answer
                </dt>
                <dd class = "col-sm-10">
                    {{quizzerAnswer.answerId}}
                </dd>
            </dl>
        </div>
        <div v-if="!loading">
            <router-link :to="'/quizzer-answer/delete/' + quizzerAnswer.id">Delete</router-link>
            <span> | </span>
            <router-link to="/quizzer-answers">Back to List</router-link>
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
import IQuizzerAnswer from '@/domain/IQuizzerAnswer';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class QuizzerAnswerDetails extends Vue {
    id!: string;
    private quizzerAnswer!: IQuizzerAnswer;
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
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quizzerAnswer = data.data!;
            }
        });
        this.loading = false;
    }
}
</script>

<style scoped>

</style>
