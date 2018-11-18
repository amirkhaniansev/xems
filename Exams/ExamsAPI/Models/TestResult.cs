using System.Collections.Generic;

namespace ExamsAPI.Models
{
    public class TestResult
    {
        public string Note { get; set; }

        public decimal? Point { get; set; }

        public List<QuestionResult> QuestionResults { get; set; }
    }
}