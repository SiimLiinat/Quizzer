export default interface IQuizAdd {
    appUserId: string;
    title: string;
    quizOrPoll: string;
    timeLimit: number | undefined;
    repeatable: boolean;
    showAnswers: boolean;
}
