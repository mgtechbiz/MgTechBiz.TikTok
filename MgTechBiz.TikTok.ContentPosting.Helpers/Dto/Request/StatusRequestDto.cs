using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;

public class StatusRequestDto
{
    [JsonPropertyName("publish_id")]
    public string PublishId { get; set; }
}