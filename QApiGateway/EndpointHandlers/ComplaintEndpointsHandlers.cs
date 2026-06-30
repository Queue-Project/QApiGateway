using System.Text;
using System.Text.Json;
using QApiGateway.DTO.ComplaintDTO;

namespace QApiGateway.EndpointHandlers;

public class ComplaintEndpointsHandlers
{
    public static async Task<IResult> CreateComplaintHandler(HttpClient client, HttpRequest httpRequest,
        ComplaintCreateRequest createRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request =
            new HttpRequestMessage(HttpMethod.Post, "http://queue-service:5006/api/Complaint/create-complaint");
        request.Headers.Add("Authorization", token);
        request.Content = new StringContent(JsonSerializer.Serialize(createRequest), Encoding.UTF8,
            "application/json");

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetComplaintHistoryHandler(HttpClient client, HttpRequest httpRequest,
        int pageNumber = 1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://queue-service:5006/api/Complaint/complaint-history/customer?pageNumber={pageNumber}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}