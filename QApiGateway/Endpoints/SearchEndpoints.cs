using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class SearchEndpoints
{
    public static void MapSearchEndpoints(this WebApplication app)
    {
        var searchGroup = app.MapGroup("/api/Search").WithTags("Search");

        searchGroup.MapGet("/full-text-search", SearchEndpointHandlers.FullTextSearch).RequireAuthorization();
        searchGroup.MapGet("/elastic-search", SearchEndpointHandlers.ElasticSearch).RequireAuthorization();
        
    }
}