<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Answer</h4>
            <hr />

            <dl class="row">
                <dt class = "col-sm-2">
                    Question
                </dt>
                <dd class = "col-sm-10">
                    {{answer.question}}
                </dd>
                <dt class = "col-sm-2">
                    Answer
                </dt>
                <dd class = "col-sm-10">
                    {{answer.answerText}}
                </dd>
                <dt class = "col-sm-2">
                    Correct
                </dt>
                <dd class = "col-sm-10">
                    {{answer.isCorrect}}
                </dd>
                <dt class = "col-sm-2">
                    Picture
                </dt>
                <dd class = "col-sm-10">
                    <img v-bind:src="answer.url">
                </dd>
            </dl>
            <div>
                <input @click="deleteAnswer" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/answer/edit/' + answer.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/answer/details/' + answer.id">Details</router-link>
            <span> | </span>
            <router-link to="/answers">Back to List</router-link>
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
import IAnswer from '@/domain/IAnswer'
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class AnswerDelete extends Vue {
    id!: string;
    private answer!: IAnswer;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

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
            }
        });
        this.loading = false;
    }

    async deleteAnswer(): Promise<void> {
        const service = new BaseService<IAnswer>('v1/answers', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/answers')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
