using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class PostResponseDataDto
{
    [JsonPropertyName("publish_id")]
    public string PublishId { get; set; }

    [JsonPropertyName("upload_url")]
    public Uri? UploadUrl { get; set; }
}