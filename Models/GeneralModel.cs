namespace QuizApp.Models
{
    public class QuizSession
    {
        public Quiz Quiz { get; set; }
        public int CurrentQuestionIndex { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public List<UserAnswer> UserAnswers { get; set; } = new List<UserAnswer>();
    }

    public class UserAnswer
    {
        public int QuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
        public bool IsCorrect { get; set; }
    }

    public class AnswerResult
    {
        public int QuestionId { get; set; }
        public int SelectedAnswerId { get; set; }
        public bool IsCorrect { get; set; }
        public int CorrectAnswerId { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
