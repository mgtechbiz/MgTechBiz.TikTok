using System.Net;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;

public interface IContentPosting
{
    Task<QueryResponseDataDto> QueryCreatorInformation();

    Task<PostResponseDto> DirectPost(DirectPostRequestDto  postRequestDto);

    Task<HttpStatusCode> UploadFile(Stream file, Uri uploadUri);

    Task<StatusResponseDto> CheckStatus(StatusRequestDto  statusRequestDto);
}