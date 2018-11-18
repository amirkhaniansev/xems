using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ExamsAPI.Global;

namespace ExamsAPI.Models
{
    public class Exam
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public DateTime Created { get; set; }

        public ExamType ExamType { get; set; }

        public string Subject { get; set; }

        public string Note { get; set; }

        public List<Test> Tests { get; set; }

        public int CreatorUserId { get; set; }

        public int CreatorProfileId { get; set; }

        public int? Duration { get; set; }

        public decimal? MaxGrade { get; set; }

        public bool HasGrade { get; set; }

        public List<int> Modifiers { get; set; }

        public List<int> Students { get; set; }
    }
}