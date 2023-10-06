using MgTechBiz.TikTok.ContentPosting.Helpers.Services;
using MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MgTechBiz.TikTok.ContentPosting.WebApp.Pages
{
	public class signin_oidcModel : PageModel
	{
		public string? Error = null;
		public string? ErrorDescription = null;

		private readonly ILogger<signin_oidcModel> _logger;
		private readonly IContentPosting _contentPosting;
		private readonly IConfiguration _config;

		public signin_oidcModel(ILogger<signin_oidcModel> logger, 
			IContentPosting contentPosting, 
			IConfiguration config)
		{
			_logger = logger;
			_contentPosting = contentPosting;
			_config = config;
		}

		public async Task<IActionResult> OnGetAsync([FromQuery] string code,
			[FromQuery] string scopes,
			[FromQuery] string state,
			[FromQuery] string error,
			[FromQuery] string error_description)
		{
			//get token
			if (!string.IsNullOrWhiteSpace(error))
			{
				Error = error;
				ErrorDescription = error_description;

				return Page();
			}

			//get and set token 
			var clientId = _config["Oidc:ClientId"];
			var clientSecret = _config["Oidc:ClientSecret"];
			var redirectUri = _config["Oidc:RedirectUri"];

			var token = await _contentPosting.GetToken(clientId, clientSecret, code, redirectUri);
			TokenService.SetToken(token);

			return RedirectToPage("Index");
			
		}
	}
}
