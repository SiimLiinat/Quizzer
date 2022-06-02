export default interface IAnswerAdd {
    questionId: string;
    isCorrect: boolean;
    answerText: string;
    url: string | undefined;
}
