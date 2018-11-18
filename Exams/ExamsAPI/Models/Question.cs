using System.Collections.Generic;
using ExamsAPI.Global;

namespace ExamsAPI.Models
{
    public class Question
    {              
        public string Text { get; set; }

        public QuestionType QuestionType { get; set; }

        public List<Variant> Variants { get; set; }

        public Variant RightVariant { get; set; }

        public decimal? Point { get; set; }
    }
}