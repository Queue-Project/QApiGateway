using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class FavoriteEmployeesEndpoints
{
    public static void MapFavoriteEmployeesEndpoints(this WebApplication app)
    {
        var employeeGroup = app.MapGroup("api/FavoriteEmployees").WithTags("FavoriteEmployees");
        employeeGroup.MapPost("/add-employee-to-favorite-list-{employeeId}", FavoriteEmployeesEndpointHandler.AddEmployeeToFavoriteList).RequireAuthorization();
        employeeGroup.MapDelete("/remove-employee-from-favorite-list-{employeeId}", FavoriteEmployeesEndpointHandler.DeleteEmployeeFromFavoriteList).RequireAuthorization();
        employeeGroup.MapGet("get-all-employees-from-favorite-list",
            FavoriteEmployeesEndpointHandler.GetAllEmployeesFromFavoriteList);
    }
}