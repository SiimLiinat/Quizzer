<template>
    <body v-if="!loading">
    <div class="container">
        <div class="row">
            <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                <div class="card card-signin my-5">
                    <div class="card-body">
                        <h5 class="card-title text-center">Edit question</h5>
                        <div class="form-signin">
                            <div class="form-label-group">
                                <input v-model="question.questionText" type="text" id="inputQuestion" class="form-control" placeholder="Question" required>
                                <label for="inputQuestion">Question</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="question.url" type="text" id="inputUrl" class="form-control" placeholder="Picture (optional)" required>
                                <label for="inputUrl">Picture (optional)</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="question.points" type="number" id="inputPoints" class="form-control" placeholder="Points" required>
                                <label for="inputPoints">Points</label>
                            </div>
                            <div class="form-label-group">
                                <input v-model="question.questionNumber" type="number" id="inputQuestionNumber" class="form-control" placeholder="Question's position (optional)" required>
                                <label for="inputQuestionNumber">Question's position (optional)</label>
                            </div>
                            <button @click="editQuestion" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                        </div>
                        <div>
                            <router-link class="mx-4" to="/questions">Back to List</router-link>
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
import IQuestion from '@/domain/IQuestion'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class QuestionEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    question!: IQuestion;

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        const service = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.question = data.data!;
            } else {
                console.log(data.statusCode);
            }
        });
        if (!this.id) await this.$router.push('/');
        this.loading = false;
    }

    async editQuestion(): Promise<void> {
        const service = new BaseService<IQuestion>('v1/questions', this.token || undefined);
        await service.put(this.id, this.question).then((data) => {
            if (data.ok) {
                this.$router.push('/questions');
            } else {
                console.log(data);
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
