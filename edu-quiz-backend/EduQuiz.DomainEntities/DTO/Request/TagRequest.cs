using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EduQuiz.DomainEntities.DTO.Request
{
    public class TagRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
