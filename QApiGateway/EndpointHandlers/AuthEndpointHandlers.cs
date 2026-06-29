using System.Text;
using System.Text.Json;
using QApiGateway.DTO.AuthDTO;

namespace QApiGateway.EndpointHandlers;

public class AuthEndpointHandlers
{
    public static async Task<IResult> LoginHandler(HttpClient client,
        LoginRequest loginRequest)
    {
        var response = await client.PostAsJsonAsync(
            "http://user-service:5004/api/Auth/login",
            loginRequest
        );
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> RegisterHandler(HttpClient client, RegisterRequest request)
    {
        var response = await client.PostAsJsonAsync(
            "http://user-service:5004/api/Auth/register", request);

        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> LogoutHandler(HttpClient client, HttpRequest httpRequest, string refreshToken)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Post, "http://user-service:5004/api/Auth/logout");
        request.Headers.Add("Authorization", token);
        request.Content = new StringContent(JsonSerializer.Serialize(refreshToken), Encoding.UTF8,
            "application/json");
            
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> VerifyEmailHandler(HttpClient client, VerifyEmailRequest request)
    {
        var response = await client.PostAsJsonAsync(
            "http://user-service:5004/api/Auth/verify-email", request);

        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> ResendCodeHandler(HttpClient client, ResendCodeRequest request)
    {
        var response = await client.PostAsJsonAsync(
            "http://user-service:5004/api/Auth/resend-code", request);

        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> ChangePasswordHandler(HttpClient client, HttpRequest httpRequest,
        UpdatePasswordRequest updatePasswordRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Put,
            "http://user-service:5004/api/Auth/change-password");
        request.Headers.Add("Authorization", token);
        request.Content = new StringContent(JsonSerializer.Serialize(updatePasswordRequest), Encoding.UTF8,
            "application/json");

        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> ForgotPasswordHandler(HttpClient client,
        ForgotPasswordRequest request)
    {
        var response = await client.PostAsJsonAsync(
            "http://user-service:5004/api/Auth/forgot-password", request);

        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> ResetPasswordHandler(HttpClient client, ResetPasswordRequest request)
    {
        var response = await client.PostAsJsonAsync(
            "http://user-service:5004/api/Auth/reset-password", request);

        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }

    public static async Task<IResult> DeleteCustomerAccountHandler(HttpClient client, HttpRequest httpRequest)
    {
        var token = httpRequest.Headers["Authorization"].ToString();
        var request = new HttpRequestMessage(HttpMethod.Delete,
            "http://user-service:5004/api/Auth/delete-customer-account");

        request.Headers.Add("Authorization", token);
        var response = await client.SendAsync(request);
        var content = await response.Content.ReadAsStringAsync();
        return Results.Content(content, "application/json", Encoding.UTF8, (int)response.StatusCode);
    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}