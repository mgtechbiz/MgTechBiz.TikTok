using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.IO.Compression;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;
using MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;

namespace MgTechBiz.TikTok.ContentPosting.WebApp.Pages;

[RequestSizeLimit(1000 * 1024 * 1024)]       //unit is bytes => 500Mb
[RequestFormLimits(MultipartBodyLengthLimit = 1000 * 1024 * 1024)]
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IContentPosting _contentPosting;

    [BindProperty]
    public FileUpload FileUpload { get; set; }

    public IndexModel(ILogger<IndexModel> logger, IContentPosting contentPosting)
    {
        _logger = logger;
        _contentPosting = contentPosting;
    }

    public void OnGet()
    {

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
}

public class FileUpload
{

    [Required]
    [Display(Name = "File")]
    public IFormFile FormFile { get; set; }

}