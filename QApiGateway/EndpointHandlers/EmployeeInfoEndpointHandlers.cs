using System.Text;

namespace QApiGateway.EndpointHandlers;

public class EmployeeInfoEndpointHandlers
{
    public static async Task<IResult> GetBranchEmployeesHandler(
        HttpClient client, 
        HttpRequest httpRequest, 
        int companyId, 
        int branchId, 
        int pageNumber=1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var query = $"?companyId={companyId}" +
                    $"&branchId={branchId}" +
                    $"&pageNumber={pageNumber}";

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://user-service:5004/api/Employee/get-branch-employees{query}");

        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetServiceEmployeesHandler(
        HttpClient client, 
        HttpRequest httpRequest, 
        int companyId, 
        int serviceId, 
        int pageNumber=1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var query = $"?companyId={companyId}" +
                    $"&serviceId={serviceId}" +
                    $"&pageNumber={pageNumber}";

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://user-service:5004/api/Employee/get-service-employees{query}");

        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}