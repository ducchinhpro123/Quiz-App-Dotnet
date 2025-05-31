using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuizApp.Models;


public class UserQuiz
{
    [Key]
    public int UserQuizID { get; set; }

    public int UserID { get; set; }
    [ForeignKey("UserID")]
    public User User { get; set; }

    public int QuizID { get; set; }
    [ForeignKey("QuizID")]
    public Quiz Quiz { get; set; }

    [DataType(DataType.Date)]
    public DateTime DateTaken { get; set; }

    public int Score { get; set; }

    public TimeSpan TotalTimeCompleted { get; set; }
}
