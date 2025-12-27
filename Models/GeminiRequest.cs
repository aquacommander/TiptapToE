using System.Collections;
using System.Text.Json.Serialization;

namespace TipTapToe.Models
{
    // Gemini API body class
    public class GeminiRequest
    {
        [JsonPropertyName("contents")]
        public required List<RequestParts> Contents { get; set; } 

        [JsonPropertyName("generationConfig")]
        public required GenerationConfig Config { get; set; } 
    }

    // Gemini API content classes
    public class RequestParts
    {
        [JsonPropertyName("parts")]
        public required ArrayList Parts { get; set; }
    }

    public class RequestPart
    {
        [JsonPropertyName("text")]
        public required string Part { get; set; }
    }

    // Gemini API config classes
    public class GenerationConfig
    {
        [JsonPropertyName("response_mime_type")]
        public required string ResponseMimeType { get; set; }

        [JsonPropertyName("response_schema")]
        public required ConfigResponseSchema ResponseSchema { get; set; }
    }

    public class ConfigResponseSchema
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }
        
        [JsonPropertyName("items")]
        public required ConfigItems Items { get; set; }
    }

    public class ConfigItems
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("properties")]
        public required ConfigProperties Properties { get; set; }
    }

    public class ConfigProperties
    {
        [JsonPropertyName("reasoning")]
        public required ConfigType Reasoning { get; set; }

        [JsonPropertyName("practiceText")]
        public required ConfigType PracticeText { get; set; }
    }

    public class ConfigType
    {
        [JsonPropertyName("type")]
        public required string Type { get; set; }
    }
    
}