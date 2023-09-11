using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class StatusResponseDto
{
    [JsonPropertyName("data")]
    public StatusDataDto Data { get; set; }

    [JsonPropertyName("error")]
    public Error Error { get; set; }
}