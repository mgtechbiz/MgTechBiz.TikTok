using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;

public class PostInfoDto
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("privacy_level")]
    public string PrivacyLevel { get; set; }

    [JsonPropertyName("disable_duet")]
    public bool DisableDuet { get; set; }

    [JsonPropertyName("disable_comment")]
    public bool DisableComment { get; set; }

    [JsonPropertyName("disable_stitch")]
    public bool DisableStitch { get; set; }

    [JsonPropertyName("video_cover_timestamp_ms")]
    public long VideoCoverTimestampMs { get; set; }
}