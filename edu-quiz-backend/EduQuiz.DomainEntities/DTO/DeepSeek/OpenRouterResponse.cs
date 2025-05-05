using System.Text.Json.Serialization;

namespace EduQuiz.DomainEntities.DTO.DeepSeek;

public class OpenRouterResponse
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonPropertyName("object")]
    public string Object { get; set; } = string.Empty;
    
    [JsonPropertyName("created")]
    public long Created { get; set; }
    
    [JsonPropertyName("model")]
    public string Model { get; set; } = string.Empty;
    
    [JsonPropertyName("choices")]
    public List<OpenRouterChoice> Choices { get; set; } = new();
    
    [JsonPropertyName("usage")]
    public OpenRouterUsage Usage { get; set; } = new();
}