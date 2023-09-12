using System.Collections.Generic;
using System.Net;
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

    public async Task<HttpStatusCode> UploadFile(Stream file, Uri uploadUri)
    {
        SetContentType("Content-Type: video/mp4");

        var chunks = await GetChunks(file);

        var chunkRange = (chunks.FirstOrDefault().chunkBytes.Length - chunks.LastOrDefault().chunkBytes.Length) / file.Length;

        HttpRequestMessage request = new(HttpMethod.Put, uploadUri);

        request.Headers.Add("Content-Range", chunkRange.ToString());

        foreach (var chunk in chunks)
        {
            request.Headers.Add("Content-Length", chunk.chunkBytes.Length.ToString());
            request.Content = new ByteArrayContent(chunk.chunkBytes);

            var response = await _httpClient.SendAsync(request);
            if (response.StatusCode is not (HttpStatusCode.PartialContent or HttpStatusCode.Created))
            {
                return response.StatusCode;
            }
        }

        return HttpStatusCode.Created;

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

    internal async Task<List<(int chunkIndex, byte[] chunkBytes)>> GetChunks(Stream file) 
    {
        List <(int position, byte[] chunkBytes)> chunks = new();

        long minimumChunkSize = 5242880; // 5 MB
        var mergeLastChunk = false;


        long chunkSize = 9437184; // 9 MB
        long totalChunks = (long)(file.Length / chunkSize);
        var remainder = Math.DivRem(file.Length, chunkSize);

        //if the remaining value is less then minimum chunk size, then it has to be merged
        if (remainder.Remainder < minimumChunkSize)
        {
            mergeLastChunk = true;
        }

        //if the remaining value is greater then minimum chunk size, then it does not need to be merged
        if (remainder.Remainder != 0 && remainder.Remainder > minimumChunkSize)
        {
            totalChunks++;
        }

        for (var i = 0; i < totalChunks; i++)
        {
            var position = (i * chunkSize);
            if (mergeLastChunk && i == totalChunks)
            {
                //merge the last chunk
                chunkSize = remainder.Remainder + chunkSize;
                position = (i * chunkSize);
            }

            var toRead = Math.Min(file.Length - position, chunkSize);

            var buffer = new byte[toRead];

            _ = await file.ReadAsync(buffer, 0, buffer.Length);

            chunks.Add((i, buffer));
        }

        return chunks;
    }

    private void SetContentType(string contentType)
    {
        _httpClient
            .DefaultRequestHeaders
            .Accept
            .Add(new MediaTypeWithQualityHeaderValue(contentType));
    }
}