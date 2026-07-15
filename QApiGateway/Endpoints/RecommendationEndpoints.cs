using System.Text;
using System.Text.Json;
using QApiGateway.DTO.ReportDTO;
using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class RecommendationEndpoints
{
    public static void MapRecommendationEndpoints(this WebApplication app)
    {
        var reportGroup = app.MapGroup("/api/Recommendation").WithTags("Recommendation");

        reportGroup.MapGet("/get-recommended-companies", RecommendationEndpointHandler.GetRecommendedCompanies)
            .RequireAuthorization();
        reportGroup.MapGet("/get-recommended-branches", RecommendationEndpointHandler.GetRecommendedBranches)
            .RequireAuthorization();
        reportGroup.MapGet("/get-recommended-services", RecommendationEndpointHandler.GetRecommendedServices)
            .RequireAuthorization();
        reportGroup.MapGet("/get-recommended-employees", RecommendationEndpointHandler.GetRecommendedEmployees)
            .RequireAuthorization();
    }
}