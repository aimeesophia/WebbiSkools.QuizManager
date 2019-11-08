using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebbiSkools.QuizManager.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class Quiz
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Question> Questions { get; set; }
    }
}
