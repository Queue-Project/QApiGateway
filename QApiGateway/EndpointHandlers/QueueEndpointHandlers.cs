using System.Text;
using System.Text.Json;
using QApiGateway.DTO.QueueDTO;

namespace QApiGateway.EndpointHandlers;

public class QueueEndpointHandlers
{
    public static async Task<IResult> GetQueueHistoryHandler(HttpClient client, HttpRequest httpRequest,
        int pageNumber = 1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var req = new HttpRequestMessage(
            HttpMethod.Get,
            $"http://localhost:5006/api/Queue/history/customer?pageNumber={pageNumber}"
        );
        req.Headers.Add("Authorization", token);

        var response = await client.SendAsync(req);
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetQueueByIdHandler(HttpClient client, HttpRequest httpRequest, int id)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var req = new HttpRequestMessage(
            HttpMethod.Get,
            $"http://localhost:5006/api/Queue/{id}"
        );
        req.Headers.Add("Authorization", token);

        var response = await client.SendAsync(req);
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> BookQueueHandler(HttpClient client, HttpRequest httpRequest,
        QueueCreateRequest request)
    {
        var token = httpRequest.Headers["Authorization"].ToString();


        var req = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5006/api/Queue/book");
        req.Headers.Add("Authorization", token);
        req.Content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );


        var response = await client.SendAsync(req);
        var content = await response.Content.ReadAsStringAsync();


        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> CancelQueueByCustomerHandler(HttpClient client, HttpRequest httpRequest,
        QueueCancelRequest request)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var req = new HttpRequestMessage(HttpMethod.Put, "http://localhost:5006/api/Queue/cancel/customer");
        req.Headers.Add("Authorization", token);
        req.Content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await client.SendAsync(req);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}