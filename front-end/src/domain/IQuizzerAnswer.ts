import IQuizzerAnswerAdd from '@/domain/IQuizzerAnswerAdd'

export default interface IQuizzerAnswer extends IQuizzerAnswerAdd {
    id: string;
    answerId: string;
    quizzerId: string;
}
