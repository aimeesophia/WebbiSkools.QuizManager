using System.Collections.Generic;

namespace WebbiSkools.QuizManager.Web.Models
{
    public class Question
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public string Text { get; set; }

        public ICollection<Answer> Answers { get; set; }
    }
}