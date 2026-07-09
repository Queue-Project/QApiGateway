using System.Text;

namespace QApiGateway.EndpointHandlers;

public class FavoriteEmployeesEndpointHandler
{
    public static async Task<IResult> AddEmployeeToFavoriteList(HttpClient client, HttpRequest httpRequest, int employeeId)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var request = new HttpRequestMessage(HttpMethod.Post,
            $"http://user-service:5004/api/FavoriteEmployees/add-employee-to-favorite-list-{employeeId}");

        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
    
    public static async Task<IResult> DeleteEmployeeFromFavoriteList(HttpClient client, HttpRequest httpRequest, int employeeId)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var request = new HttpRequestMessage(HttpMethod.Delete,
            $"http://user-service:5004/api/FavoriteEmployees/remove-employee-from-favorite-list-{employeeId}");

        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
    
    public static async Task<IResult> GetAllEmployeesFromFavoriteList(HttpClient client, HttpRequest httpRequest, int pageNumber=1)
    {
        var token = httpRequest.Headers["Authorization"].ToString();

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://user-service:5004/api/FavoriteEmployees/get-all-employees-from-favorite-list?pageNumber={pageNumber}");

        request.Headers.Add("Authorization", token);

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();

        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
}