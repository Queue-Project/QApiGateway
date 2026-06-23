using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class EmployeeInfoEndpoints
{
    public static void MapEmployeeInfoEndpoints(this WebApplication app)
    {
        var employeeGroup = app.MapGroup("api/Employee").WithTags("Employee");
        employeeGroup.MapGet("/get-branch-employees", EmployeeInfoEndpointHandlers.GetBranchEmployeesHandler).RequireAuthorization();
        employeeGroup.MapGet("/get-service-employees", EmployeeInfoEndpointHandlers.GetServiceEmployeesHandler).RequireAuthorization();
    }
}