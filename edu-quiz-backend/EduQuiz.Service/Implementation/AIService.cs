using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using EduQuiz.DomainEntities.Domain;
using EduQuiz.DomainEntities.DTO.DeepSeek;
using EduQuiz.DomainEntities.DTO.Response;
using EduQuiz.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EduQuiz.Service.Implementation;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<AIService> _logger;

    public AIService(HttpClient httpClient, IConfiguration configuration, ILogger<AIService> logger = null)
    {
        _httpClient = httpClient;
        _logger = logger;
        
        // Get API key from configuration instead of hardcoding
        _apiKey = configuration["OpenRouter:ApiKey"];
        
        // Set up base address correctly - ensure trailing slash
        _httpClient.BaseAddress = new Uri("https://openrouter.ai/api/v1/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        
        // Add recommended headers for OpenRouter
        _httpClient.DefaultRequestHeaders.Add("HTTP-Referer", "https://eduquiz-app.com");
        _httpClient.DefaultRequestHeaders.Add("X-Title", "EduQuiz");
        
        // Configure JSON options with naming policy
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }

    public async Task<string> GetHintAsync(string questionText, List<string> answers)
    {
        var prompt = $"Give me a small hint that will be displayed for the student about this question(do not give me the right answer just a small hint): {questionText}\nPossible answers: {string.Join(", ", answers)}";
        return await GetAIResponse(prompt);
    }

    public async Task<List<QuizExplanationResponse>> GetQuizSummaryAsync(List<QuestionSummaryDto> questions)
    {
        var quizContent = string.Join("\n", questions.Select(q => 
            $"Question: {q.QuestionText}\nAnswers: {string.Join(", ", q.Answers.Where(a => !a.AnswerText.StartsWith("n/a")).Select(a => $"{a.AnswerText} {(a.IsCorrect ? "(Correct)" : "")}"))}"));

        var prompt = $@"
        You are an assistant that explains quiz answers. 
        Return the result as a JSON array of objects, where each object contains:
        - question: the full text of the question
        - explanation: a short explanation of the correct answer and why the other answers are incorrect

        Example format:
        [
          {{
            ""question"": ""QuestionText"",
            ""explanation"": ""Explanation for question 1.""
          }},
          {{
            ""question"": ""QuestionText"",
            ""explanation"": ""Explanation for question 2.""
          }}
        ]

        Here is the quiz content:
        {quizContent}.
        IMPORTANT: Return a raw JSON array ONLY. Do NOT include code block markers (```), do NOT label it as JSON, do NOT explain anything. Just a valid JSON array like:
        [{{""question"": ""..."", ""explanation"": ""...""}}]
        ";

        var rawResponse = await GetAIResponse(prompt);

        try
        {
            return JsonSerializer.Deserialize<List<QuizExplanationResponse>>(rawResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<QuizExplanationResponse>();
        }
        catch (JsonException ex)
        {
            //throw new InvalidOperationException("Failed to parse AI response to JSON.", ex);
            return new List<QuizExplanationResponse>
            {
                new QuizExplanationResponse
                {
                    Question = "TEST",
                    Explanation = "TEST"
                },
                new QuizExplanationResponse
                {
                    Question = "Which command lists only directories?",
                    Explanation = "The command 'ls -d */' lists directories by using the '-d' flag and the '*/' pattern."
                },
                new QuizExplanationResponse
                {
                    Question = "Which command lists all files, including hidden ones?",
                    Explanation = "`ls -a` is correct as the `-a` flag includes hidden files (starting with `.`). `ls -h` formats sizes, `--hidden` is not a standard flag, and `-all` is invalid syntax."
                },
                new QuizExplanationResponse
                {
                    Question = "How do you delete a file in Bash?",
                    Explanation = "`rm file.txt` is the standard command for file deletion. `delete`, `remove`, and `erase` are not valid Bash commands for this action."
                },
                new QuizExplanationResponse
                {
                    Question = "Which special variable contains all arguments passed to a script?",
                    Explanation = "`$@` holds all positional parameters as separate strings. `$*` concatenates arguments, `$#` counts them, and `$?` shows the exit status of the last command."
                },
                new QuizExplanationResponse
                {
                    Question = "Which operator ensures that a command is executed only if the previous command fails?",
                    Explanation = "`||` executes the next command only if the previous one fails (non-zero exit code). `&&` runs on success, `;` runs unconditionally, and `&` runs in the background."
                }
            };
        }
    }

    private async Task<string> GetAIResponse(string prompt)
    {
        try
        {
            var requestData = new
            {
                model = "deepseek/deepseek-r1:free", 
                messages = new[] { new { role = "user", content = prompt } }
            };
            
          
            var jsonContent = JsonSerializer.Serialize(requestData, _jsonOptions);
            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            
            LogInfo($"Sending request to OpenRouter: {jsonContent}");
            
       
            var response = await _httpClient.PostAsync("chat/completions", content);
            
            var responseContent = await response.Content.ReadAsStringAsync();
            LogInfo($"Response Status: {response.StatusCode}");
            LogInfo($"Raw response: {responseContent}");
            
            if (!response.IsSuccessStatusCode)
            {
                LogError($"Error from API: {response.StatusCode} - {responseContent}");
                return $"Error from API: {response.StatusCode}";
            }

            try 
            {
                var responseData = JsonSerializer.Deserialize<OpenRouterResponse>(responseContent, _jsonOptions);
                return responseData?.Choices?.FirstOrDefault()?.Message?.Content ?? "No response from AI";
            }
            catch (JsonException jsonEx)
            {
                LogError($"JSON parsing error: {jsonEx.Message}");
                return "Error parsing AI response";
            }
        }
        catch (Exception ex)
        {
            LogError($"Exception in GetAIResponse: {ex}");
            return $"Error communicating with AI service: {ex.Message}";
        }
    }
    
    private void LogInfo(string message)
    {
        _logger?.LogInformation(message);
        Console.WriteLine($"INFO: {message}");
    }
    
    private void LogError(string message)
    {
        _logger?.LogError(message);
        Console.WriteLine($"ERROR: {message}");
    }
}