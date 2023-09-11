using System.Net.Http.Headers;
using System.Net.Http.Json;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Request;
using MgTechBiz.TikTok.ContentPosting.Helpers.Dto.Response;
using MgTechBiz.TikTok.ContentPosting.Helpers.Services.Contracts;

namespace MgTechBiz.TikTok.ContentPosting.Helpers.Services;

public class ContentPosting : IContentPosting
{
    private readonly HttpClient _httpClient;

    public ContentPosting(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<QueryResponseDataDto> QueryCreatorInformation()
    {
        SetContentType("application/json; charset=UTF-8");

        HttpRequestMessage request = new(HttpMethod.Post, @"https://open.tiktokapis.com/v2/post/publish/creator_info/query/");

        var response = await _httpClient.SendAsync(request);

        var content = await response.Content.ReadFromJsonAsync<QueryResponseDataDto>();

        return content;

    }

    public async Task<PostResponseDto> DirectPost(DirectPostRequestDto postRequestDto)
    {
        SetContentType("application/json; charset=UTF-8");

        HttpRequestMessage request = new(HttpMethod.Post, @"https://open.tiktokapis.com/v2/post/publish/video/init/");

        request.Content = JsonContent.Create(postRequestDto, new MediaTypeHeaderValue("application/json", "UTF-8"));

        var response = await _httpClient.SendAsync(request);

        var content = await response.Content.ReadFromJsonAsync<PostResponseDto>();

        return content;
    }

    public async Task<StatusResponseDto> CheckStatus(StatusRequestDto statusRequestDto)
    {
        SetContentType("application/json; charset=UTF-8");

        HttpRequestMessage request = new(HttpMethod.Post, @"https://open.tiktokapis.com/v2/post/publish/status/fetch/");

        request.Content = JsonContent.Create(statusRequestDto, new MediaTypeHeaderValue("application/json", "UTF-8"));

        var response = await _httpClient.SendAsync(request);

        var content = await response.Content.ReadFromJsonAsync<StatusResponseDto>();

        return content;
    }

    private void SetContentType(string contentType)
    {
        _httpClient
            .DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue(contentType));
    }
}