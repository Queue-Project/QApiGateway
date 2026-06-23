using System.Text;

namespace QApiGateway.EndpointHandlers;

public class CompanyEndpointsHandlers
{
    public static async Task<IResult> GetAllCompaniesHandler(HttpClient client, HttpRequest httpRequest,
        int pageNumber = 1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://localhost:5002/api/Company/get-all-companies?pageNumber={pageNumber}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetCompanyInfoById(HttpClient client, HttpRequest httpRequest, int id)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://localhost:5002/api/Company/company-info-by-id/{id}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}