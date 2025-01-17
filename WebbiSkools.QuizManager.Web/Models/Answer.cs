﻿using System.Diagnostics.CodeAnalysis;

namespace WebbiSkools.QuizManager.Web.Models
{
    [ExcludeFromCodeCoverage]
    public class Answer
    {
        public int Id { get; set; }

        public int QuestionId { get; set; }

        public string Text { get; set; }
    }
}