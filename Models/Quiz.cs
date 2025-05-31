using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QuizApp.Models;

/*Quiz ↔ Question (one-to-many)
Question ↔ Answer (one-to-many)
User ↔ UserQuiz (one-to-many)
Quiz ↔ UserQuiz (one-to-many)*/

public class Quiz
{
    [Key]
    public int QuizID { get; set; }
    public string QuizTitle { get; set; }
    public ICollection<Question> Questions { get; set; }
    public ICollection<UserQuiz> UserQuizzes { get; set; }
}
