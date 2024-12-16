using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaApp.Models
{
    public class TriviaQuestion
    {
      
        [System.Text.Json.Serialization.JsonPropertyName("type")]
        public string Type { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("difficulty")]
        public string Difficulty { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("category")]
        public string Category { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("question")]
        public string Question { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("correct_answer")]
        public string CorrectAnswer { get; set; }

        [System.Text.Json.Serialization.JsonPropertyName("incorrect_answers")]
        public List<string> IncorrectAnswers { get; set; }
    }
}
