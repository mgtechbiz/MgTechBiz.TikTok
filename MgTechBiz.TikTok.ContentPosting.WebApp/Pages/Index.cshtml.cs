using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using System.Net;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;
using MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;
using Microsoft.AspNetCore.WebUtilities;

namespace MgTechBiz.TikTok.ContentPosting.WebApp.Pages;

[RequestSizeLimit(1000 * 1024 * 1024)]       //unit is bytes => 500Mb
[RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]
public class IndexModel : PageModel
{
	private readonly ILogger<IndexModel> _logger;
	private readonly IContentPosting _contentPosting;
	private readonly IConfiguration _config;

	[BindProperty]
	public FileUpload FileUpload { get; set; }

	[BindProperty]
	public TokenResponseDto? Token { get; set; }  

	public IndexModel(ILogger<IndexModel> logger, IContentPosting contentPosting, IConfiguration config)
	{
		_logger = logger;
		_contentPosting = contentPosting;
		_config = config;
	}

	public void OnGet()
	{
		_logger.LogInformation("OnGet called");
	}


	public async Task<IActionResult> OnPostUploadAsync()
	{
		//save the file to a memory stream
		using var memoryStream = new MemoryStream();
		await FileUpload.FormFile.CopyToAsync(memoryStream);

		//query user 
		var info = await _contentPosting.QueryCreatorInformation();

		//post video
		var post = await _contentPosting.DirectPost(new DirectPostRequestDto
		{
			PostInfo = new PostInfoDto
			{

			},
			SourceInfo = new SourceInfoDto
			{

			}
		});

		//post actual video
		if (post.Data.UploadUrl != null)
		{
			var upload = _contentPosting.UploadFile(memoryStream, post.Data.UploadUrl);
		}


		return Page();
	}

	public IActionResult OnPostToken()
	{
		var authority = _config["Oidc:Authority"];
		var clientId = _config["Oidc:ClientId"];
		var clientSecret = _config["Oidc:ClientSecret"];
		var redirectUri = _config["Oidc:RedirectUri"];
		var state = Guid.NewGuid().ToString(format: "N");

		var queryParams = new Dictionary<string, string>
		{
			{ "client_key", clientId },
			{ "scope", clientId },
			{ "redirect_uri", redirectUri },
			{ "state", state },
			{ "response_type", "code" }
		};
		//var uri = WebUtility.UrlEncode(QueryHelpers.AddQueryString(authority, queryParams));
		var uri = QueryHelpers.AddQueryString(authority, queryParams);

		return Redirect(uri);
	}
}

public class FileUpload
{

	[Required]
	[Display(Name = "File")]
	public IFormFile FormFile { get; set; }

}