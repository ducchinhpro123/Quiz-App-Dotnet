using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuizApp.Models;
using QuizApp.Data;

using System.Text.Json;

namespace QuizApp.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly QuizAppDbContext _context;

        public QuizzesController(QuizAppDbContext context)
        {
            _context = context;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {

            var s = await _context.Quiz.Include(q => q.Questions).ToListAsync();

            // return View(await _context.Quiz.ToListAsync());
            return View(s);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitQuiz(int quizId, Dictionary<int, int> userAnswers, string quizStartTime)
        {
            Console.WriteLine(quizStartTime);
            if (userAnswers == null || !userAnswers.Any())
            {
                TempData["ErrorMsg"] = "No answers were submitted";

                // public virtual RedirectToActionResult RedirectToAction(string? actionName, object? routeValues);
                // TakeQuiz/:quizId
                return RedirectToAction("TakeQuiz", new { id = quizId });
            }
            DateTime startTimeUtc;
            // From RoundtripKind
            // Summary:
            //     The System.DateTimeKind field of a date is preserved when a System.DateTime object
            //     is converted to a string using the "o" or "r" standard format specifier, and
            //     the string is then converted back to a System.DateTime object.
            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings#Roundtrip
            DateTime.TryParse(quizStartTime, null, System.Globalization.DateTimeStyles.RoundtripKind, out startTimeUtc);
            var endTime = DateTime.UtcNow;
            TimeSpan timeTaken = endTime - startTimeUtc;
            var timeTakenString = timeTaken.ToString(@"hh\:mm\:ss");

            // Question ID - Answer ID
            var SelectedAnswerIds = userAnswers.Values.ToList();
            var correctSubmitted = await _context.Answer.Where(a => SelectedAnswerIds.Contains(a.AnswerID) && a.IsCorrect).ToListAsync();
            int correctAnswer = correctSubmitted.Count; // How many answer correct?

            /*
             *TempData["QuizIdForSummary"] = quizId;
             *TempData["UserScore"] = correctAnswer;
             *TempData["TotalQuestionsAttempted"] = userAnswers.Count;
             *TempData["TimeTaken"] = timeTakenString;
             */

            var quiz = _context.Quiz.Include(q => q.Questions)
                .ThenInclude(a => a.Answers).FirstOrDefault(m => m.QuizID == quizId);
            // This filter the question that user answer incorrect

            var questionAnsweredIncorrect = new List<Question>();

            foreach (var q in quiz.Questions)
            {
                if (userAnswers.TryGetValue(q.QuestionID, out int userAnswerId))
                {
                    var answer = q.Answers.FirstOrDefault(a => a.AnswerID == userAnswerId);
                    if (answer != null && !answer.IsCorrect)
                    {
                        questionAnsweredIncorrect.Add(q);
                    }
                }
                else
                {
                    questionAnsweredIncorrect.Add(q);
                }
            }

            quiz.Questions = questionAnsweredIncorrect;

            var viewModel = new QuizSummaryViewModel
            {
                QuizID = quizId,
                Quiz = quiz,
                UserAnswers = userAnswers,
                Score = correctAnswer,
                TotalQuestions = userAnswers.Count,
                TimeTaken = timeTakenString
            };
            Console.WriteLine($"Time taken: {timeTakenString}");

            return View("Summary", viewModel);
            // return RedirectToAction("Summary");
            // return Content($"Quiz ${quizId} submitted, you answer {userAnswers.Count}, correct anwer: { correctAnswer }");
        }

        /*public async Task<IActionResult> Summary()
        {
            if (
                TempData["QuizIdForSummary"] == null ||
                TempData["UserScore"] == null ||
                TempData["TotalQuestionsAttempted"] == null
                )
            {
                return RedirectToAction("Index");
            }

            var quiz = await _context.Quiz.Include(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(m => m.QuizID == (int)TempData["QuizIdForSummary"]);

            ViewBag.QuizId = TempData["QuizIdForSummary"];
            ViewBag.Score = TempData["UserScore"];
            ViewBag.TotalQuestions = TempData["TotalQuestionsAttempted"];

            var userAnswersJson = TempData["UserAnswers"]?.ToString();
            Dictionary<int, int> userAnswers = null;
            if (!string.IsNullOrEmpty(userAnswersJson))
            {
                userAnswers = JsonSerializer.Deserialize<Dictionary<int, int>>(userAnswersJson);
            }

            ViewBag.UserAnswers = userAnswers;

            return View(quiz);
        }*/

        // GET: Quizzes/TakeQuiz/:1?
        public async Task<IActionResult> TakeQuiz(int? id)
        {
            if (id == null) NotFound();

            var quiz = await _context.Quiz.Include(q => q.Questions)
                .ThenInclude(a => a.Answers)
                .FirstOrDefaultAsync(m => m.QuizID == id);

            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizzes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizID,QuizTitle")] Quiz quiz)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quizzes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizID,QuizTitle")] Quiz quiz)
        {
            if (id != quiz.QuizID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.QuizID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quiz
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quiz.FindAsync(id);
            if (quiz != null)
            {
                _context.Quiz.Remove(quiz);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
            return _context.Quiz.Any(e => e.QuizID == id);
        }
    }
}
