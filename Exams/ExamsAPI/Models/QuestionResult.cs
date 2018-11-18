namespace ExamsAPI.Models
{
    public class QuestionResult
    {
        public string Note { get; set; }

        public string Answer { get; set; }

        public decimal? Point { get; set; }

        public Variant RightVariant { get; set; }
    }
}