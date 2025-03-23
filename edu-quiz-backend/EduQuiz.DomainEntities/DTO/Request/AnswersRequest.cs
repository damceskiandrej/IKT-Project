using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Request
{
    public class AnswersRequest
    {
        [JsonPropertyName("answer_a")]
        public string Answer_A { get; set; }

        [JsonPropertyName("answer_b")]
        public string Answer_B { get; set; }

        [JsonPropertyName("answer_c")]
        public string Answer_C { get; set; }

        [JsonPropertyName("answer_d")]
        public string Answer_D { get; set; }

        [JsonPropertyName("answer_e")]
        public string Answer_E { get; set; }

        [JsonPropertyName("answer_f")]
        public string Answer_F { get; set; }
    }
}
