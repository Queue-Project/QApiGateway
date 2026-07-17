using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class QueueTrackingEndpoints
{
    public static void MapQueueTrackingEndpoints(this WebApplication app)
    {
        var reportGroup = app.MapGroup("/api/QueueTracking").WithTags("QueueTracking");

        reportGroup.MapGet("/get-customer-active-queues/{customerId}",
                QueueTrackingEndpointHandlers.GetCustomerActiveQueues)
            .RequireAuthorization();
        reportGroup.MapGet("get-queue-position/{queueId}", QueueTrackingEndpointHandlers.GetQueuePosition)
            .RequireAuthorization();
    }
}