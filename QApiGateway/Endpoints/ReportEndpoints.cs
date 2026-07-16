using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this WebApplication app)
    {
        var reportGroup = app.MapGroup("/api/Report").WithTags("Report");

        reportGroup.MapGet("/customer-report", ReportEndpointHandlers.GetCustomerReport).RequireAuthorization();
    }
}