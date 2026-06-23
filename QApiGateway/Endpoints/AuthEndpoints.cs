using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var authGroup = app.MapGroup("/api/Auth").WithTags("Auth");

        authGroup.MapPost("/login", AuthEndpointHandlers.LoginHandler);

        authGroup.MapPost("/register", AuthEndpointHandlers.RegisterHandler);

        authGroup.MapPost("/logout", AuthEndpointHandlers.LogoutHandler).RequireAuthorization();

        authGroup.MapPost("/verify-email", AuthEndpointHandlers.VerifyEmailHandler);

        authGroup.MapPost("/resend-code", AuthEndpointHandlers.ResendCodeHandler);

        authGroup.MapPut("/change-password", AuthEndpointHandlers.ChangePasswordHandler);

        authGroup.MapPost("/forgot-password", AuthEndpointHandlers.ForgotPasswordHandler);

        authGroup.MapPost("/reset-password", AuthEndpointHandlers.ResetPasswordHandler);

        authGroup.MapDelete("/delete-customer-account", AuthEndpointHandlers.DeleteCustomerAccountHandler)
            .RequireAuthorization();
    }
}