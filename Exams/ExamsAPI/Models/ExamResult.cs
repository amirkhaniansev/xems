using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamsAPI.Models
{
    public class ExamResult
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ExamId { get; set; }

        public string Note { get; set; }
        
        public decimal? Grade { get; set; }

        public DateTime Created { get; set; }

        public List<TestResult> TestResults { get; set; }

        public bool IsChecked { get; set; }
    }
}