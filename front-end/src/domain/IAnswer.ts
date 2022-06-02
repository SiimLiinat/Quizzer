import IAnswerAdd from '@/domain/IAnswerAdd'

export default interface IAnswer extends IAnswerAdd {
    id: string;
    questionId: string;
    answerText: string;
    isCorrect: boolean;
    url: string | undefined;
    question: string;
    answerers: string[];
}
