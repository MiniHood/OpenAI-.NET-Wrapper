using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

class OpenAI
{
    private readonly string _apiKey;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _baseUrl = "https://api.openai.com/v1/engines/gpt-3/jobs";

    public OpenAI(string apiKey)
    {
        _apiKey = apiKey;
    }

    public async Task<string> GenerateText(string prompt, int maxTokens = 100)
    {
        var request = new GenerateTextRequest
        {
            Prompt = prompt,
            MaxTokens = maxTokens
        };

        var response = await PostJsonAsync("generate", request);
        var generateTextResponse = JsonConvert.DeserializeObject<GenerateTextResponse>(response);

        return generateTextResponse.Choices[0].Text;
    }

    private async Task<string> PostJsonAsync(string endpoint, object content)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        _httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        var response = await _httpClient.PostAsync($"{_baseUrl}/{endpoint}", new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json"));

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Request to OpenAI failed with status code {response.StatusCode}: {await response.Content.ReadAsStringAsync()}");
        }

        return await response.Content.ReadAsStringAsync();
    }
}

class GenerateTextRequest
{
    public string Prompt { get; set; }
    public int MaxTokens { get; set; }
}

class GenerateTextResponse
{
    public Choice[] Choices { get; set; }
}

class Choice
{
    public string Text { get; set; }
}
