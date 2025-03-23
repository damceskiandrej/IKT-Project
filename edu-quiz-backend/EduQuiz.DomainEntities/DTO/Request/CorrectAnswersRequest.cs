using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Request
{
    public class CorrectAnswersRequest
    {
        [JsonPropertyName("answer_a_correct")]
        public bool Answer_A_Correct { get; set; }

        [JsonPropertyName("answer_b_correct")]
        public bool Answer_B_Correct { get; set; }

        [JsonPropertyName("answer_c_correct")]
        public bool Answer_C_Correct { get; set; }

        [JsonPropertyName("answer_d_correct")]
        public bool Answer_D_Correct { get; set; }

        [JsonPropertyName("answer_e_correct")]
        public bool Answer_E_Correct { get; set; }

        [JsonPropertyName("answer_f_correct")]
        public bool Answer_F_Correct { get; set; }
    }
}
