<template>
    <body v-if="!loading">
        <div class="container">
            <div class="row">
                <div class="col-sm-9 col-md-7 col-lg-5 mx-auto">
                    <div class="card card-signin my-5">
                        <div class="card-body">
                            <h5 class="card-title text-center">Edit answer</h5>
                            <div class="form-signin">
                                <div class="form-label-group">
                                    <input v-model="answer.answerText" type="text" id="inputTime" class="form-control" required placeholder="Answer">
                                    <label for="inputTime">Answer</label>
                                </div>
                                <div class="form-label-group">
                                    <input v-model="answer.url" type="text" id="inputReview" class="form-control" placeholder="Picture (optional)">
                                    <label for="inputReview">Picture (optional)</label>
                                </div>
                                <div class="custom-control custom-checkbox mb-3">
                                    <input v-model="answer.isCorrect" type="checkbox" class="custom-control-input" id="customCheck1">
                                    <label class="custom-control-label" for="customCheck1">Correct</label>
                                </div>
                                <button @click="editAnswer" class="btn btn-lg btn-primary btn-block text-uppercase" type="submit">Create</button>
                            </div>
                            <div>
                                <router-link class="mx-4" to="/answers">Back to List</router-link>
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
import IAnswer from '@/domain/IAnswer'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class AnswerEdit extends Vue {
    id!: string;
    private loading: boolean = true;

    answer!: IAnswer;

    get token(): string | undefined {
        return store.state.token;
    }

    get userId(): string | undefined {
        return store.state.id;
    }

    async mounted(): Promise<void> {
        if (this.userId === undefined) await this.$router.push('/');
        const service = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.answer = data.data!;
            } else {
                console.log(data.statusCode)
            }
        });
        this.loading = false;
    }

    async editAnswer(): Promise<void> {
        const service = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await service.put(this.id, this.answer).then((data) => {
            if (data.ok) {
                this.$router.push('/answers')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped src="../identity/identity.css">

</style>
