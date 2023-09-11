using System.Text.Json.Serialization;
using MgTechBiz.TikTok.ContentPosting.Helpers.Enums;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class StatusDataDto
{
    [JsonPropertyName("status")]
    public PostStatus Status { get; set; }

    [JsonPropertyName("fail_reason")]
    public string FailReason { get; set; }

    [JsonPropertyName("publicaly_available_post_id")]
    public List<long> PublicalyAvailablePostId { get; set; }

    [JsonPropertyName("uploaded_bytes")]
    public long UploadedBytes { get; set; }
    
    [JsonPropertyName("downloaded_bytes")]
    public long DownloadedBytes { get; set; }
}