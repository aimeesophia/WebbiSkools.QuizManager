using Microsoft.EntityFrameworkCore;
using WebbiSkools.QuizManager.Web.Models;

namespace WebbiSkools.QuizManager.Web.Data
{
    public class QuizManagerContext : DbContext
    {
        public QuizManagerContext(DbContextOptions<QuizManagerContext> options)
            : base(options)
        {
        }

        public DbSet<Quiz> Quizzes { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<Answer> Answers { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
