using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;

namespace QuizApp.Data
{
    public class QuizAppDbContext : DbContext
    {
        public QuizAppDbContext (DbContextOptions<QuizAppDbContext> options)
            : base(options)
        {
        }

        public DbSet<QuizApp.Models.Quiz> Quiz { get; set; } = default!;
        public DbSet<QuizApp.Models.Answer> Answer { get; set; } = default!;
        public DbSet<QuizApp.Models.Question> Question { get; set; } = default!;
        public DbSet<QuizApp.Models.UserQuiz> UserQuiz { get; set; } = default!;
        public DbSet<QuizApp.Models.User> User { get; set; } = default!;
    }
}
