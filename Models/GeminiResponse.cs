using System.Text.Json.Serialization;

namespace TipTapToe.Models
{

  public class GeminiResponse
  {
    [JsonPropertyName("candidates")]
    public required List<ResponseContent> Candidates { get; set; }
  }

  public class ResponseContent
  {
    [JsonPropertyName("content")]
    public required ResponseParts Content { get; set; }
  }

  public class ResponseParts
  {
    [JsonPropertyName("parts")]
    public required List<ResponsePart> Parts { get; set; }
  }

  public class ResponsePart
  {
    [JsonPropertyName("text")]
    public required string Part { get; set; }
  }

  public class ResponseText
  {
    [JsonPropertyName("practiceText")]
    public required string PracticeText { get; set; }

    [JsonPropertyName("reasoning")]
    public required string Reasoning { get; set; }
  }

}