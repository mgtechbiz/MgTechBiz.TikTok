using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;

public class DirectPostRequestDto
{
    [JsonPropertyName("post_info")]
    public PostInfoDto PostInfo { get; set; }

    [JsonPropertyName("source_info")]
    public SourceInfoDto SourceInfo { get; set; }
}