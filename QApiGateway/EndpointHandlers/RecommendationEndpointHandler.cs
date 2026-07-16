using System.Text;
using QApiGateway.DTO.RecommendationDTO;

namespace QApiGateway.EndpointHandlers;

public class RecommendationEndpointHandler
{
    public static async Task<IResult> GetRecommendedCompanies(HttpClient client, HttpRequest httpRequest,
        [AsParameters] RecommendedCompanyRequest reportRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?CategoryId={reportRequest.CategoryId}" +
                    $"&PageNumber={reportRequest.PageNumber}";
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://aggregation-service:5008/api/Recommendation/get-recommended-companies{query}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetRecommendedBranches(HttpClient client, HttpRequest httpRequest,
        [AsParameters] RecommendedBranchRequest reportRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?CompanyId={reportRequest.CompanyId}" +
                    $"&PageNumber={reportRequest.PageNumber}";
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://aggregation-service:5008/api/Recommendation/get-recommended-branches{query}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetRecommendedServices(HttpClient client, HttpRequest httpRequest,
        [AsParameters] RecommendedServiceRequest reportRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?CompanyId={reportRequest.CompanyId}" +
                    $"&BranchId={reportRequest.BranchId}" +
                    $"&PageNumber={reportRequest.PageNumber}";
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://aggregation-service:5008/api/Recommendation/get-recommended-services{query}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetRecommendedEmployees(HttpClient client, HttpRequest httpRequest,
        [AsParameters] RecommendedEmployeeRequest reportRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?CompanyId={reportRequest.CompanyId}" +
                    $"&BranchId={reportRequest.BranchId}" +
                    $"&ServiceId={reportRequest.ServiceId}" +
                    $"&PageNumber={reportRequest.PageNumber}";
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://aggregation-service:5008/api/Recommendation/get-recommended-employees{query}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}