using QApiGateway.EndpointHandlers;

namespace QApiGateway.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this WebApplication app)
    {
        var companyGroup = app.MapGroup("api/Company").WithTags("Company");
        companyGroup.MapGet("/get-all-companies", CompanyEndpointsHandlers.GetAllCompaniesHandler).RequireAuthorization();
        companyGroup.MapGet("/company-info-by-id/{id}", CompanyEndpointsHandlers.GetCompanyInfoById).RequireAuthorization();
    }
}