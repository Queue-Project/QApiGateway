using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class QueueEndpoints
{
    public static void MapQueueEndpoints(this WebApplication app)
    {
        var queueGroup = app.MapGroup("/api/Queue").WithTags("Queue");

        queueGroup.MapGet("/history/customer",QueueEndpointHandlers.GetQueueHistoryHandler).RequireAuthorization();

        queueGroup.MapGet("{id}", QueueEndpointHandlers.GetQueueByIdHandler).RequireAuthorization();
        
        queueGroup.MapPost("/book", QueueEndpointHandlers.BookQueueHandler).RequireAuthorization();
        
        queueGroup.MapPut("/cancel/customer", QueueEndpointHandlers.CancelQueueByCustomerHandler).RequireAuthorization();
    }
}