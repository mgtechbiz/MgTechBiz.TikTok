using System.Text.Json.Serialization;
using MgTechBiz.TikTok.ContentPosting.Helpers.Enums;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;

public class SourceInfoDto
{
    [JsonPropertyName("source")]
    public PostSource Source { get; set; }

    [JsonPropertyName("video_size")]
    public long? VideoSize { get; set; }

    [JsonPropertyName("chunk_size")]
    public long? ChunkSize { get; set; }

    [JsonPropertyName("total_chunk_count")]
    public long? TotalChunkCount { get; set; }

    [JsonPropertyName("video_url")]
    public string? VideoUrl { get; set; }
}