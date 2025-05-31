using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace QuizApp.Models;

public class User
{
    //
    // Summary: From key
    //     Initializes a new instance of the System.ComponentModel.DataAnnotations.KeyAttribute
    //     class.
    [Key]
    public int UserId { get;set; }
    public string Username { get; set; }

    public ICollection<UserQuiz> UserQuizzes { get; set; }
}
