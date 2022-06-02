import IQuizAdd from '@/domain/IQuizAdd'

export default interface IQuiz extends IQuizAdd {
    id: string;
    appUserId: string;
    title: string;
    quizOrPoll: string;
    createdAt: string;
    timeLimit: number | undefined;
    repeatable: boolean;
    showAnswers: boolean;

    appUserName: string;
    pointsSum: number;
}
