using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class ReviewEndpoints
{
    public static void MapReviewEndpoints(this WebApplication app)
    {
        var reviewGroup = app.MapGroup("/api/Review").WithTags("Review");

        reviewGroup.MapPost("create-review", ReviewEndpointHandlers.CreateReview).RequireAuthorization();

        reviewGroup.MapGet("review-history/customer", ReviewEndpointHandlers.GetCustomerReviewHistory)
            .RequireAuthorization();
    }
}