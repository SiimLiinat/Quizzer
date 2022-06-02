import IQuestionAdd from '@/domain/IQuestionAdd'

export default interface IQuestion extends IQuestionAdd {
    id: string;
    quizId: string;
    questionText: string;
    url: string | undefined;
    points: number | undefined;
    questionNumber: number | undefined;

    quizTitle: string;
    hasCorrectAnswer: boolean;
    totalAnswers: number;
    totalQuizzerAnswers: number;
}
