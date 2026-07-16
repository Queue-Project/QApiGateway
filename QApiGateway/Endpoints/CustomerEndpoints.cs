using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class CustomerEndpoints
{
    public static void MapCustomerEndpoints(this WebApplication app)
    {
        var customerGroup = app.MapGroup("api/Customer").WithTags("Customer");

        customerGroup.MapGet("/get-customer-profile", CustomerEndpointsHandlers.GetCustomerProfile).RequireAuthorization();
        customerGroup.MapPut("/customer-profile-update", CustomerEndpointsHandlers.UpdateCustomerProfile).RequireAuthorization();
        
    }
}