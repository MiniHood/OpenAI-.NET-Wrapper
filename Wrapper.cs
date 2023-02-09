using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OpenAIWrapper
{
    public class OpenAI
    {
        private readonly string _apiKey;
        private readonly string _engine;
        private readonly HttpClient _httpClient;

        public OpenAI(string apiKey, string engine)
        {
            _apiKey = apiKey;
            _engine = engine;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.openai.com/v1/")
            };
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GenerateText(string prompt, int maxTokens = 100, float temperature = 1.0f)
        {
            var request = new GenerateTextRequest
            {
                Engine = _engine,
                Prompt = prompt,
                MaxTokens = maxTokens,
                Temperature = temperature
            };

            var response = await PostJsonAsync("engines/engine/generate", request);
            var generateTextResponse = JsonConvert.DeserializeObject<GenerateTextResponse>(response);

            return generateTextResponse.Choices[0].Text;
        }

        private async Task<string> PostJsonAsync(string url, object data)
        {
            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

    class GenerateTextRequest
    {
        public string Engine { get; set; }
        public string Prompt { get; set; }
        public int MaxTokens { get; set; }
        public float Temperature { get; set; }
    }

    class GenerateTextResponse
    {
        public Choice[] Choices { get; set; }
    }

    class Choice
    {
        public string Text { get; set; }
    }
}
