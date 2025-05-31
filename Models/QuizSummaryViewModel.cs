namespace QuizApp.Models
{
    public class QuizSummaryViewModel
    {
        public int QuizID {get; set; }
        public Quiz Quiz { get; set; }
        public Dictionary<int, int> UserAnswers { get; set; }
        public int Score { get; set; }
        public int TotalQuestions { get; set; }
        public string TimeTaken { get; set; }
    }
}
