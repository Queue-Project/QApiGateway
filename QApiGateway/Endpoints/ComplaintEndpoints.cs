using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class ComplaintEndpoints
{
    public static void MapComplaintEndpoints(this WebApplication app)
    {
        var complaintGroup = app.MapGroup("api/Complaint").WithTags("Complaint");
        complaintGroup.MapPost("create-complaint", ComplaintEndpointsHandlers.CreateComplaintHandler).RequireAuthorization();
        complaintGroup.MapGet("complaint-history/customer", ComplaintEndpointsHandlers.GetComplaintHistoryHandler).RequireAuthorization();
    }
}