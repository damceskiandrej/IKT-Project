using EduQuiz.DomainEntities.Domain;
using EduQuiz.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EduQuiz.Repository.Implementation
{
    public class DeepSeekRepository : Repository<Reccomendation>, IDeepSeekRepository
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _deepSeekUrl = "http://localhost:11434/api/generate";
        public DeepSeekRepository(ApplicationDbContext context, IHttpClientFactory httpClientFactory) : base(context) 
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GenerateResponseAsync(string prompt)
        {
             var requestBody = new
            {
                model = "deepseek-r1:1.5b",
                prompt = prompt,
                stream = false
            };

            //var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync(_deepSeekUrl, requestBody);
            response.EnsureSuccessStatusCode();

            var responseJson = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseJson);
            var message = doc.RootElement.GetProperty("response").GetString();

            return message ?? string.Empty;
        }

    }
}
