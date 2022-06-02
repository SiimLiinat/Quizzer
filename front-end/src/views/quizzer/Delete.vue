<template>
    <div class="card card-body mt-5">
        <div v-if="!loading" class="card card-body">
            <h4>Quizzer</h4>
            <hr />
            <dl class="row">
                <dt class = "col-sm-2">
                    User
                </dt>
                <dd class = "col-sm-10">
                    {{quizzer.appUserName}}
                </dd>
                <dt class = "col-sm-2">
                    Quiz Title
                </dt>
                <dd class = "col-sm-10">
                    {{quizzer.quizTitle}}
                </dd>
                <dt class = "col-sm-2">
                    Started at
                </dt>
                <dd class = "col-sm-10">
                    {{quizzer.startedAt}}
                </dd>
                <dt class = "col-sm-2">
                    Points
                </dt>
                <dd class = "col-sm-10">
                    {{quizzer.points}}
                </dd>
            </dl>
            <div>
                <input @click="deleteQuizzer" type="submit" value="Delete" class="btn btn-danger" />
            </div>
        </div>
        <div v-if="!loading">
            <router-link :to="'/quizzer/edit/' + quizzer.id">Edit</router-link>
            <span> | </span>
            <router-link :to="'/quizzer/details/' + quizzer.id">Details</router-link>
            <span> | </span>
            <router-link to="/quizzers">Back to List</router-link>
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
import IQuizzer from '@/domain/IQuizzer';
@Options({
    components: {},
    props: {
        id: String,
    }
})
export default class QuizzerDelete extends Vue {
    id!: string;
    private quizzer!: IQuizzer;
    private loading: boolean = true;

    get role(): string | undefined {
        return store.state.role;
    }

    get token(): string | undefined {
        return store.state.token;
    }

    async mounted(): Promise<void> {
        if (this.role !== 'Admin') await this.$router.push('/');
        const service = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await service.get(this.id).then((data) => {
            if (data.ok) {
                this.quizzer = data.data!;
            }
        });
        this.loading = false;
    }

    async deleteQuizzer(): Promise<void> {
        const service = new BaseService<IQuizzer>('v1/quizzers', this.token || undefined);
        await service.delete(this.id).then((data) => {
            if (data.ok) {
                this.$router.push('/quizzers')
            } else {
                console.log(data)
            }
        });
    }
}
</script>

<style scoped>

</style>
