using System.Text;
using System.Text.Json;
using QApiGateway.DTO.ReviewDTO;

namespace QApiGateway.EndpointHandlers;

public class ReviewEndpointHandlers
{
    public static async Task<IResult> CreateReview(HttpClient client, HttpRequest httpRequest,
        ReviewCreateRequest createRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:5006/api/Review/create-review");
        request.Headers.Add("Authorization", token);
        request.Content = new StringContent(JsonSerializer.Serialize(createRequest),
            Encoding.UTF8,
            "application/json");

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> GetCustomerReviewHistory(HttpClient client, HttpRequest httpRequest,
        int pageNumber = 1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://localhost:5006/api/Review/review-history/customer?pageNumber={pageNumber}");

        request.Headers.Add("Authorization", token);


        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}