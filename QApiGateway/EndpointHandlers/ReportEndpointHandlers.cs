using System.Text;
using QApiGateway.DTO.ReportDTO;

namespace QApiGateway.EndpointHandlers;

public class ReportEndpointHandlers
{
    public static async Task<IResult> GetCustomerReport(HttpClient client, HttpRequest httpRequest,
       [AsParameters] CustomerReportRequest reportRequest)
    {
        
        var token = httpRequest.Headers["Authorization"].ToString();

        var query = $"?CustomerId={reportRequest.CustomerId}" +
                    $"&FromDate={reportRequest.FromDate}" +
                    $"&ToDate={reportRequest.ToDate}";
                
        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5008/api/Report/customer-report{query}");
        request.Headers.Add("Authorization", token);
                
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}