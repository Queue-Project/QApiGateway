using System.Text;
using QApiGateway.DTO;

namespace QApiGateway.EndpointHandlers;

public static class SearchEndpointHandlers
{
    public static async Task<IResult> FullTextSearch(HttpClient client, HttpRequest httpRequest,
        [AsParameters] SearchRequest searchRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?SearchTerm={searchRequest.SearchTerm}" +
                    $"&PageNumber={searchRequest.PageNumber}" +
                    $"&PageSize={searchRequest.PageSize}";

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://search-service:5088/api/Search/full-text-search{query}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
    
    public static async Task<IResult> ElasticSearch(HttpClient client, HttpRequest httpRequest,
        [AsParameters] SearchRequest searchRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?SearchTerm={searchRequest.SearchTerm}" +
                    $"&PageNumber={searchRequest.PageNumber}" +
                    $"&PageSize={searchRequest.PageSize}";

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://search-service:5088/api/Search/elastic-search{query}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
    
}