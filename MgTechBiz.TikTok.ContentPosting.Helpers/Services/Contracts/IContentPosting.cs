using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;

public interface IContentPosting
{
    Task<QueryResponseDataDto> QueryCreatorInformation();

    Task<PostResponseDto> DirectPost(DirectPostRequestDto  postRequestDto);

    Task<StatusResponseDto> CheckStatus(StatusRequestDto  statusRequestDto);
}