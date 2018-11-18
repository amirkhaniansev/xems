using System;
using System.Collections.Generic;
namespace ExamsAPI.Models
{
    public class Test
    {
        public int CreatorId { get; set; }

        public int? Duration { get; set; }

        public decimal? Point { get; set; }

        public DateTime Created { get; set; }

        public List<Question> Questions { get; set; }
    }
}