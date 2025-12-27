using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TipTapToe.Models;

namespace TipTapToe.Services
{
  public class GeminiApiService
  {

    // Constructor for creating HttpClient instance and Gemini URI 
    private readonly HttpClient httpClient;
    private readonly string geminiUri;
    public GeminiApiService()
    {
      httpClient = new HttpClient();
      httpClient.DefaultRequestHeaders.Accept.Clear();
      httpClient.DefaultRequestHeaders.Accept.Add(
          new MediaTypeWithQualityHeaderValue("application/json")
      );
      string? geminiApiKey = Environment.GetEnvironmentVariable("GEMINI_API_KEY");
      if (geminiApiKey == null)
      {
        throw new InvalidOperationException("Unable to find Gemini API Key.");
      }
      geminiUri = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent?key={geminiApiKey}";
    }

    // Simplified test request without structured output
    public async Task<string> TestRequest()
    {
      Console.WriteLine("SimpleTestRequest is running in GeminiApiService.cs");
      try
      {
        var simpleRequest = new
        {
          contents = new[]
          {
            new
            {
                parts = new[]
                {
                    new { text = "Explain how AI works in a few words" }
                }
            }
          }
        };
        JsonContent requestBodyJson = JsonContent.Create(simpleRequest);
        using HttpResponseMessage response = await httpClient.PostAsync(geminiUri, requestBodyJson);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status: {response.StatusCode}");
        Console.WriteLine($"Response: {responseContent}");
        return responseContent;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Unexpected error: {ex}");
        throw;
      }
    }

    // API template for making request and handling response
    public async Task<string> MakeApiRequest(RequestParts requestParts)
    {
      try
      {
        GeminiRequest requestBody = new()
        {
          Contents =
            [
                requestParts
            ],
          Config = new GenerationConfig
          {
            ResponseMimeType = "application/json",
            ResponseSchema = new ConfigResponseSchema
            {
              Type = "ARRAY",
              Items = new ConfigItems
              {
                Type = "OBJECT",
                Properties = new ConfigProperties
                {
                  Reasoning = new ConfigType
                  {
                    Type = "STRING"
                  },
                  PracticeText = new ConfigType
                  {
                    Type = "STRING"
                  }
                }
              }
            }
          }
        };
        JsonContent requestBodyJson = JsonContent.Create(requestBody);
        using HttpResponseMessage response = await httpClient.PostAsync(geminiUri, requestBodyJson);
        string responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
          var responseContentJson = JsonSerializer.Deserialize<GeminiResponse>(responseContent);
          if (responseContentJson == null)
          {
            throw new InvalidOperationException("Error deserializing Gemini API response");
          }
          string responseData = responseContentJson.Candidates[0].Content.Parts[0].Part;
          var responseDataJson = JsonSerializer.Deserialize<List<ResponseText>>(responseData);
          if (responseDataJson == null || responseDataJson.Count == 0)
          {
            throw new InvalidOperationException("Error deserializing data in Gemini API response");
          }
          string geminiPracticeText = responseDataJson[0].PracticeText;
          return geminiPracticeText;
        }
        else
        {
          throw new HttpRequestException($"Gemini API request failed. Status: {response.StatusCode}. Body: {responseContent}");
        }
      }
      catch
      {
        throw;
      }
    }

    // Prompt for generating assessment string
    public async Task<string> GenerateAssessment(string language)
    {
      RequestParts requestParts = new()
      {
        Parts = [
          new RequestPart
          {
            Part = $"You're a typing coach for {language} developers. Generate a unique sequence of {language} code that will be used to assess their current typing skills. The code should be readable with valid spacing, syntax, meaningful variable or function names, and realistic patterns typical for {language}. It MUST be a single line, that is 40-50 characters long, with only single spaces."
          }

        ]
      };
      string geminiPracticeText = await MakeApiRequest(requestParts);
      return geminiPracticeText;
    }

    // Prompt for initial assessment
    public async Task<string> GeneratePractice(List<LogItem> keyLog, string language)
    {
      RequestParts requestParts = new()
      {
        Parts =
          [
              new RequestPart
                    {
                        Part = $"You're a typing coach for {language} developers. Analyse this keystroke data for slow transitions (>150ms between keystrokes) and error patterns (backspace usage). Based on the keystroke data, generate a unique line of {language} code for your student to practice with. The code should be readable with valid spacing, syntax, meaningful variable or function names, and realistic patterns typical for {language}. It MUST be a single line, that is 40-50 characters long, with only single spaces, that is completely different to what is in the keystroke data."
                    },
                    new RequestPart
                    {
                        Part = JsonSerializer.Serialize(keyLog)
                    }
          ]
      };
      string geminiPracticeText = await MakeApiRequest(requestParts);
      return geminiPracticeText;
    }

  }
}