using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class QueryResponseDataDto
{
    [JsonPropertyName("creator_avatar_url")]
    public Uri CreatorAvatarUrl { get; set; }

    [JsonPropertyName("creator_username")]
    public string CreatorUsername { get; set; }

    [JsonPropertyName("creator_nickname")]
    public string CreatorNickname { get; set; }

    [JsonPropertyName("privacy_level_options")]
    public List<string> PrivacyLevelOptions { get; set; }

    [JsonPropertyName("comment_disabled")]
    public bool CommentDisabled { get; set; }

    [JsonPropertyName("duet_disabled")]
    public bool DuetDisabled { get; set; }

    [JsonPropertyName("stitch_disabled")]
    public bool StitchDisabled { get; set; }

    [JsonPropertyName("max_video_post_duration_sec")]
    public long MaxVideoPostDurationSec { get; set; }
}