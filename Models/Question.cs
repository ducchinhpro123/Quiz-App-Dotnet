using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QuizApp.Models;

public class Question
{
    [Key]
    public int QuestionID { get; set; }
    public string QuestionText { get; set; }

    public int QuizID { get; set; }
    [ForeignKey("QuizID")]
    public Quiz Quiz { get; set; }
    public ICollection<Answer> Answers { get; set; }
}
