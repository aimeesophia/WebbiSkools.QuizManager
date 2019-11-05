using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Web.Data
{
    public class DbInitialiser
    {
        public static void Initialise(QuizManagerContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any() || context.Quizzes.Any() || context.Questions.Any() || context.Answers.Any())
            {
                return;
            }

            var users = new User[]
            {
                new User() {Username = "ViewPermissionsUser", Password = "KsZI989Bq+ce7My2vz+9xNt/XRtx6GAWM9dwZr0cX5w=", Role = "View"},
                new User() {Username = "RestrictedPermissionsUser", Password = "Hd0MMdsBKWOwCOG6W6HqdoAdIa9Z2ydtTq6OMn9Kaw8=", Role = "Restricted"},
                new User() {Username = "EditPermissionsUser", Password = "HVu5S85JnAa/lRbzu387Hyveq3iKBt5HIrAYxa4beME=", Role = "Edit"}
            };

            foreach (var user in users)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();

            var quizzes = new Quiz[]
            {
                new Quiz() {Title = "The Software Development Lifecycle"},
                new Quiz() {Title = "C#"},
                new Quiz() {Title = "Agile Workflow"}
            };

            foreach (var quiz in quizzes)
            {
                context.Quizzes.Add(quiz);
            }

            context.SaveChanges();

            var questions = new Question[]
            {
                new Question() {QuizId = quizzes.Single(x => x.Title == "The Software Development Lifecycle").Id, Text = "At which point in the software development lifecycle is a system design document produced?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "The Software Development Lifecycle").Id, Text = "How many stages are there in the software development lifecycle?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "The Software Development Lifecycle").Id, Text = "Which SDLC phase is associated with data modeling?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "The Software Development Lifecycle").Id, Text = "Which of the following models defines the outside (actors) and inside of the system's behaviour?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "C#").Id, Text = "Which of the following keywords is used for including a namespace within the program?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "C#").Id, Text = "The C# keyword 'int' maps to which .NET type?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "C#").Id, Text = "If a method is marked as protected internal, who can access it?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "C#").Id, Text = "In Object Oriented Programming, which answer best describes encapsulation?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "Agile Workflow").Id, Text = "Which of the following is delivered at the end of a sprint?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "Agile Workflow").Id, Text = "The product backlog should be ordered on the basis of what?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "Agile Workflow").Id, Text = "When is a sprint retrospective ceremony performed?"},
                new Question() {QuizId = quizzes.Single(x => x.Title == "Agile Workflow").Id, Text = "Who is responsible for measuring the project’s performance?"}
            };

            foreach (var question in questions)
            {
                context.Questions.Add(question);
            }

            context.SaveChanges();

            var answers = new Answer[]
            {
                new Answer() {QuestionId = questions.Single(x => x.Text == "At which point in the software development lifecycle is a system design document produced?").Id, Text = "Deployment/implementation."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "At which point in the software development lifecycle is a system design document produced?").Id, Text = "Design."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "At which point in the software development lifecycle is a system design document produced?").Id, Text = "Code development."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "At which point in the software development lifecycle is a system design document produced?").Id, Text = "Feasibility study."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "How many stages are there in the software development lifecycle?").Id, Text = "4."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "How many stages are there in the software development lifecycle?").Id, Text = "5."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "How many stages are there in the software development lifecycle?").Id, Text = "7."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "How many stages are there in the software development lifecycle?").Id, Text = "10."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which SDLC phase is associated with data modeling?").Id, Text = "Analysis."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which SDLC phase is associated with data modeling?").Id, Text = "Maintenance."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which SDLC phase is associated with data modeling?").Id, Text = "Design."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which SDLC phase is associated with data modeling?").Id, Text = "None of the above."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following models defines the outside (actors) and inside of the system's behaviour?").Id, Text = "Class modeling."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following models defines the outside (actors) and inside of the system's behaviour?").Id, Text = "Test model."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following models defines the outside (actors) and inside of the system's behaviour?").Id, Text = "Domain object model."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following models defines the outside (actors) and inside of the system's behaviour?").Id, Text = "Use case model."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following keywords is used for including a namespace within the program?").Id, Text = "Imports."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following keywords is used for including a namespace within the program?").Id, Text = "Using."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following keywords is used for including a namespace within the program?").Id, Text = "Exports."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following keywords is used for including a namespace within the program?").Id, Text = "None of the above."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The C# keyword 'int' maps to which .NET type?").Id, Text = "System.Int16"},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The C# keyword 'int' maps to which .NET type?").Id, Text = "System.Int32"},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The C# keyword 'int' maps to which .NET type?").Id, Text = "System.Int64"},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The C# keyword 'int' maps to which .NET type?").Id, Text = "System.Int128"},
                new Answer() {QuestionId = questions.Single(x => x.Text == "If a method is marked as protected internal, who can access it?").Id, Text = "Classes that are both in the same assembly and derived from the declaring class."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "If a method is marked as protected internal, who can access it?").Id, Text = "Only methods that are in the same class as the method in question."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "If a method is marked as protected internal, who can access it?").Id, Text = "Internal methods can only be called using reflection."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "If a method is marked as protected internal, who can access it?").Id, Text = "Classes within the same assembly, and classes derived from the declaring class."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "In Object Oriented Programming, which answer best describes encapsulation?").Id, Text = "The conversion of one type of object to another."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "In Object Oriented Programming, which answer best describes encapsulation?").Id, Text = "The runtime resolution of method calls."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "In Object Oriented Programming, which answer best describes encapsulation?").Id, Text = "The exposition of data."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "In Object Oriented Programming, which answer best describes encapsulation?").Id, Text = "The separation of interface and implementation."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following is delivered at the end of a sprint?").Id, Text = "A document containing test cases for the current sprint."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following is delivered at the end of a sprint?").Id, Text = "An architectural design of the solution."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following is delivered at the end of a sprint?").Id, Text = "An increment of finished software."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Which of the following is delivered at the end of a sprint?").Id, Text = "Wireframes designs for user interface."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The product backlog should be ordered on the basis of what?").Id, Text = "The value of the items being delivered."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The product backlog should be ordered on the basis of what?").Id, Text = "The complexity of the items being delivered."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The product backlog should be ordered on the basis of what?").Id, Text = "Size of the items being delivered."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "The product backlog should be ordered on the basis of what?").Id, Text = "The risk associated with the items."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "When is a sprint retrospective ceremony performed?").Id, Text = "Whenever the team suggests."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "When is a sprint retrospective ceremony performed?").Id, Text = "At the end of each sprint."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "When is a sprint retrospective ceremony performed?").Id, Text = "Whenever needed."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "When is a sprint retrospective ceremony performed?").Id, Text = "Whenever the product owner suggests."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Who is responsible for measuring the project’s performance?").Id, Text = "The scrum master."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Who is responsible for measuring the project’s performance?").Id, Text = "The delivery manager."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Who is responsible for measuring the project’s performance?").Id, Text = "The product owner."},
                new Answer() {QuestionId = questions.Single(x => x.Text == "Who is responsible for measuring the project’s performance?").Id, Text = "The development team."}
            };

            foreach (var answer in answers)
            {
                context.Answers.Add(answer);
            }

            context.SaveChanges();
        }
    }
}
