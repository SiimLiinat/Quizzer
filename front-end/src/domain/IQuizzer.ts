import IQuizzerAdd from '@/domain/IQuizzerAdd'

export default interface IQuizzer extends IQuizzerAdd {
    id: string;
    appUserId: string;
    quizId: string;
    startedAt: string;
    finishedAt: string | undefined;
    points: number;
    quizTitle: string;
    appUserName: string;
    quizPercentage: number;
}
