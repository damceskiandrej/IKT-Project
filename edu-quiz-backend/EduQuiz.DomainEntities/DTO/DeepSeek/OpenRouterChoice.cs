using System.Text.Json.Serialization;


namespace EduQuiz.DomainEntities.DTO.DeepSeek;

public class OpenRouterChoice
{
    [JsonPropertyName("message")]
    public OpenRouterMessage Message { get; set; } = new();
    
    [JsonPropertyName("index")]
    public int Index { get; set; }
    
    [JsonPropertyName("finish_reason")]
    public string FinishReason { get; set; } = string.Empty;
}