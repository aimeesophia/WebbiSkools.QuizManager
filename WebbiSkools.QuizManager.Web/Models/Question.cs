using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace WebbiSkools.QuizManager.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class Question
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Text { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}