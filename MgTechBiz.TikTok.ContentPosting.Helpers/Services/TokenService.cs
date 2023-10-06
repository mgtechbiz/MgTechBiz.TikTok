using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Services;

public static class TokenService
{
	internal static TokenResponseDto? tokenResponse;

	public static string? GetToken()
	{
		return tokenResponse?.AccessToken;
	}

	public static void SetToken(TokenResponseDto token)
	{
		tokenResponse = token;
	}
}