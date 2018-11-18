using System.Collections.Generic;

namespace ExamsAPI.Models
{
    public class TestAnswer
    {
        public string Note { get; set; }

        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}