using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ExamsAPI.Models
{
    public class ExamAnswer
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string ExamId { get; set; }

        public int StudentId { get; set; }

        public DateTime Created { get; set; }

        public List<TestAnswer> TestResults { get; set; }
    }
}