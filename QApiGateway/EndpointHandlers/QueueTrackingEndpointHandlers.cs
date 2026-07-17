using System.Text;

namespace QApiGateway.EndpointHandlers;

public class QueueTrackingEndpointHandlers
{
    public static async Task<IResult> GetCustomerActiveQueues(HttpClient client, HttpRequest httpRequest,
        int customerId)
    {
        var token = httpRequest.Headers["Authorization"].ToString();


        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://aggregation-service:5008/api/QueueTracking/get-customer-active-queues/{customerId}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetQueuePosition(HttpClient client, HttpRequest httpRequest,
        int queueId)
    {
        var token = httpRequest.Headers["Authorization"].ToString();


        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://aggregation-service:5008/api/QueueTracking/get-queue-position/{queueId}");
        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}