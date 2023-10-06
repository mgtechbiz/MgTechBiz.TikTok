using System.Text.Json.Serialization;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

public class TokenResponseDto
{
	[JsonPropertyName("access_token")]
	public string AccessToken { get; set; }

	[JsonPropertyName("expires_in")]
	public long ExpiresIn { get; set; }

	[JsonPropertyName("open_id")]
	public string OpenId { get; set; }

	[JsonPropertyName("refresh_expires_in")]
	public long RefreshExpiresIn { get; set; }

	[JsonPropertyName("refresh_token")]
	public string RefreshToken { get; set; }

	[JsonPropertyName("scope")]
	public string Scope { get; set; }

	[JsonPropertyName("token_type")]
	public string TokenType { get; set; }
}