using System.Text.Json.Serialization;

namespace EduQuiz.DomainEntities.DTO.DeepSeek;

public class OpenRouterMessage
{
    [JsonPropertyName("role")]
    public string Role { get; set; } = string.Empty;
    
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}