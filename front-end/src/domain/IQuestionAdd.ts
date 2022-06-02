export default interface IQuestionAdd {
    quizId: string;
    questionText: string;
    url: string | undefined;
    points: number | undefined;
    questionNumber: number | undefined;
}
