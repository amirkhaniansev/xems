using System;
using ExamsAPI.Global;

namespace ExamsAPI.Filters
{
    public class ExamFilter : FilterBase
    {
        public ExamType? ExamType { get; set; }

        public int? CreatorUserId { get; set; }

        public int? CreatorProfileId { get; set; }

        public int? Duration { get; set; }

        public decimal? MaxGrade { get; set; }

        public bool? HasGrade { get; set; }

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}