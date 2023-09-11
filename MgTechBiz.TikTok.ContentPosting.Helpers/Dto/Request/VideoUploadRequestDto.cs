using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;

public class VideoUploadRequestDto
{
    [JsonPropertyName("source_info")]
    public SourceInfoDto SourceInfo { get; set; }
}