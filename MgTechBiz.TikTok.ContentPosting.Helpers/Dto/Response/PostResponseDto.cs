using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class PostResponseDto
{
    [JsonPropertyName("data")]
    public PostResponseDataDto Data { get; set; }

    [JsonPropertyName("error")]
    public ErrorResponseDto Error { get; set; }
}