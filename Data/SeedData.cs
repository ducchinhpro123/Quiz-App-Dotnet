using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuizApp.Data;
using System;
using System.Linq;

namespace QuizApp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new QuizAppDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<QuizAppDbContext>>()))
        {
            if (context.Quiz.Any())
            {
                return;
            }

            context.Quiz.AddRange(
                    new Quiz
                    {
                        QuizTitle = "C Programming Networking",
                        Questions = new List<Question>
                        {

                            new Question
                            {
                                QuestionText = "Which function is used to create a socket in C networking?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "socket()", IsCorrect = true },
                                    new Answer { AnswerText = "connect()", IsCorrect = false },
                                    new Answer { AnswerText = "bind()", IsCorrect = false },
                                    new Answer { AnswerText = "listen()", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "What does the 'bind()' function do?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "Establishes a connection with a server", IsCorrect = false },
                                    new Answer { AnswerText = "Associates a socket with a specific local IP address and port number", IsCorrect = true },
                                    new Answer { AnswerText = "Listens for incoming connections", IsCorrect = false },
                                    new Answer { AnswerText = "Sends data over a socket", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "Which header file is essential for socket programming in C?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "<stdio.h>", IsCorrect = false },
                                    new Answer { AnswerText = "<stdlib.h>", IsCorrect = false },
                                    new Answer { AnswerText = "<sys/socket.h>", IsCorrect = true },
                                    new Answer { AnswerText = "<string.h>", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "What does the 'listen()' function do in socket programming?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "Creates a new socket", IsCorrect = false },
                                    new Answer { AnswerText = "Puts the server socket in passive mode to accept incoming connections", IsCorrect = true },
                                    new Answer { AnswerText = "Connects to a remote server", IsCorrect = false },
                                    new Answer { AnswerText = "Sends data to a client", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "Which function is used to accept incoming connections?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "connect()", IsCorrect = false },
                                    new Answer { AnswerText = "accept()", IsCorrect = true },
                                    new Answer { AnswerText = "recv()", IsCorrect = false },
                                    new Answer { AnswerText = "send()", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "What is the purpose of the 'connect()' function?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "To bind a socket to an address", IsCorrect = false },
                                    new Answer { AnswerText = "To establish a connection to a remote server", IsCorrect = true },
                                    new Answer { AnswerText = "To listen for incoming connections", IsCorrect = false },
                                    new Answer { AnswerText = "To create a new socket", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "Which function is used to send data over a socket?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "recv()", IsCorrect = false },
                                    new Answer { AnswerText = "send()", IsCorrect = true },
                                    new Answer { AnswerText = "accept()", IsCorrect = false },
                                    new Answer { AnswerText = "listen()", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "What does TCP stand for?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "Transfer Control Protocol", IsCorrect = false },
                                    new Answer { AnswerText = "Transmission Control Protocol", IsCorrect = true },
                                    new Answer { AnswerText = "Transport Communication Protocol", IsCorrect = false },
                                    new Answer { AnswerText = "Terminal Connection Protocol", IsCorrect = false }
                                }
                            },
                            new Question
                            {
                                QuestionText = "Which socket type is used for TCP connections?",
                                Answers = new List<Answer>
                                {
                                    new Answer { AnswerText = "SOCK_DGRAM", IsCorrect = false },
                                    new Answer { AnswerText = "SOCK_STREAM", IsCorrect = true },
                                    new Answer { AnswerText = "SOCK_RAW", IsCorrect = false },
                                    new Answer { AnswerText = "SOCK_PACKET", IsCorrect = false }
                                }
                            },
                        }
                    }
            );
            context.SaveChanges();
        }
    }
}
