using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class CreatorQueryResponseDto
{
    [JsonPropertyName("data")]
    public QueryResponseDataDto Data { get; set; }

    [JsonPropertyName("error")]
    public ErrorResponseDto Error { get; set; }
}