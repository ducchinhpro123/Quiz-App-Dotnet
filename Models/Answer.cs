using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace QuizApp.Models;


public class Answer
{
    [Key]
    public int AnswerID { get; set; }
    public string AnswerText { get; set; }
    public bool IsCorrect { get; set; }

    public int QuestionID { set; get; }
    [ForeignKey("QuestionID")]
    public Question Question { set; get; }
}
