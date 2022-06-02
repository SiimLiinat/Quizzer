import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import Home from '../views/Home.vue';
import Login from '@/views/identity/Login.vue';
import Register from '@/views/identity/Register.vue';
import RoleIndex from '@/views/app-role/Index.vue';
import RoleCreate from '@/views/app-role/Create.vue';
import RoleDetails from '@/views/app-role/Details.vue';
import RoleEdit from '@/views/app-role/Edit.vue';
import RoleDelete from '@/views/app-role/Delete.vue';
import UserRoleCreate from '@/views/app-user-role/Create.vue';
import UserRoleDelete from '@/views/app-user-role/Delete.vue';
import UserIndex from '@/views/app-user/Index.vue';
import UserDetails from '@/views/app-user/Details.vue';
import UserDelete from '@/views/app-user/Delete.vue';
import UserEdit from '@/views/app-user/Edit.vue';
import AnswerIndex from '@/views/answer/Index.vue'
import AnswerCreate from '@/views/answer/Create.vue'
import AnswerDetails from '@/views/answer/Details.vue'
import AnswerEdit from '@/views/answer/Edit.vue'
import AnswerDelete from '@/views/answer/Delete.vue'
import QuestionIndex from '@/views/question/Index.vue'
import QuestionCreate from '@/views/question/Create.vue'
import QuestionDetails from '@/views/question/Details.vue'
import QuestionEdit from '@/views/question/Edit.vue'
import QuestionDelete from '@/views/question/Delete.vue'
import QuizIndex from '@/views/quiz/Index.vue'
import QuizCreate from '@/views/quiz/Create.vue'
import QuizDetails from '@/views/quiz/Details.vue'
import QuizEdit from '@/views/quiz/Edit.vue'
import QuizDelete from '@/views/quiz/Delete.vue'
import QuizzerDelete from '@/views/quizzer/Delete.vue'
import QuizzerDetails from '@/views/quizzer/Details.vue'
import QuizzerCreate from '@/views/quizzer/Create.vue'
import QuizzerIndex from '@/views/quizzer/Index.vue'
import QuizzerAnswerIndex from '@/views/quizzer-answer/Index.vue'
import QuizzerAnswerCreate from '@/views/quizzer-answer/Create.vue'
import QuizzerAnswerDetails from '@/views/quizzer-answer/Details.vue'
import QuizzerAnswerDelete from '@/views/quizzer-answer/Delete.vue'

const routes: Array<RouteRecordRaw> = [
    {
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/register',
        name: 'Register',
        component: Register
    },
    {
        path: '/login',
        name: 'Login',
        component: Login
    },
    // answers
    { path: '/answers', name: 'answers', component: AnswerIndex },
    { path: '/answer/create', name: 'answer-create', component: AnswerCreate },
    { path: '/answer/details/:id', name: 'answer-details', component: AnswerDetails, props: true },
    { path: '/answer/edit/:id', name: 'answer-edit', component: AnswerEdit, props: true },
    { path: '/answer/delete/:id', name: 'answer-delete', component: AnswerDelete, props: true },
    // questions
    { path: '/questions', name: 'questions', component: QuestionIndex },
    { path: '/question/create', name: 'question-create', component: QuestionCreate },
    { path: '/question/details/:id', name: 'question-details', component: QuestionDetails, props: true },
    { path: '/question/edit/:id', name: 'question-edit', component: QuestionEdit, props: true },
    { path: '/question/delete/:id', name: 'question-delete', component: QuestionDelete, props: true },
    // quizzes
    { path: '/quizzes', name: 'quizzes', component: QuizIndex },
    { path: '/quiz/create', name: 'quiz-create', component: QuizCreate },
    { path: '/quiz/details/:id', name: 'quiz-details', component: QuizDetails, props: true },
    { path: '/quiz/edit/:id', name: 'quiz-edit', component: QuizEdit, props: true },
    { path: '/quiz/delete/:id', name: 'quiz-delete', component: QuizDelete, props: true },
    // quizzers
    { path: '/quizzers', name: 'quizzers', component: QuizzerIndex },
    { path: '/quizzer/create', name: 'quizzer-create', component: QuizzerCreate },
    { path: '/quizzer/details/:id', name: 'quizzer-details', component: QuizzerDetails, props: true },
    { path: '/quizzer/delete/:id', name: 'quizzer-delete', component: QuizzerDelete, props: true },
    // quizzer-answers
    { path: '/quizzer-answers', name: 'quizzer-answers', component: QuizzerAnswerIndex },
    { path: '/quizzer-answer/create', name: 'quizzer-answer-create', component: QuizzerAnswerCreate },
    { path: '/quizzer-answer/details/:id', name: 'quizzer-answer-details', component: QuizzerAnswerDetails, props: true },
    { path: '/quizzer-answer/delete/:id', name: 'quizzer-answer-delete', component: QuizzerAnswerDelete, props: true },
    // roles
    { path: '/roles', name: 'roles', component: RoleIndex },
    { path: '/role/create', name: 'role-create', component: RoleCreate },
    { path: '/role/details/:id', name: 'role-details', component: RoleDetails, props: true },
    { path: '/role/edit/:id', name: 'role-edit', component: RoleEdit, props: true },
    { path: '/role/delete/:id', name: 'role-delete', component: RoleDelete, props: true },
    // user-roles
    { path: '/user-role/create', name: 'user-role-create', component: UserRoleCreate },
    { path: '/user-role/delete', name: 'user-role-delete', component: UserRoleDelete },
    // users
    { path: '/users', name: 'users', component: UserIndex },
    { path: '/user/details/:id', name: 'user-details', component: UserDetails, props: true },
    { path: '/user/edit/:id', name: 'user-edit', component: UserEdit, props: true },
    { path: '/user/delete/:id', name: 'user-delete', component: UserDelete, props: true },
    {
        path: '/quiz/:id',
        name: 'Quiz',
        props: true,
        component: () => import(/* webpackChunkName: "about" */ '../views/quiz/QuizView.vue')
    },
    {
        path: '/quiz/take/:id',
        name: 'QuizTake',
        props: true,
        component: () => import(/* webpackChunkName: "about" */ '../views/quiz/QuizTake.vue')
    },
    {
        path: '/quiz/results/:id',
        name: 'QuizResults',
        props: true,
        component: () => import(/* webpackChunkName: "about" */ '../views/quiz/QuizResults.vue')
    },
    {
        path: '/profile/:id',
        name: 'Profile',
        props: true,
        component: () => import(/* webpackChunkName: "about" */ '../views/profile/Overview.vue')
    },
    {
        path: '/about',
        name: 'About',
        // route level code-splitting
        // this generates a separate chunk (about.[hash].js) for this route
        // which is lazy-loaded when the route is visited.
        component: () => import(/* webpackChunkName: "about" */ '../views/About.vue')
    }
]

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes,
})

export default router
