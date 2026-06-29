using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using QApiGateway.EndpointHandlers;
using QApiGateway.Endpoints;
using Shouldly;

namespace QApiGateway.UnitTest.EndpointTests.CompanyEndpointTests;

public class GetAllCompaniesTests
{
    
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly WebApplication _app;

    public GetAllCompaniesTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(_httpClient);
        _app = builder.Build();
        _app.MapCompanyEndpoints();
    }

    [Fact]
    public async Task GetAllCompanies_Should_Return_Companies_When_Authorized()
    {
        //Arrange

        int pageNumber = 1;
        var expectedResponse = "{companies: id:1, companyName:Test Name}";
        var token = "Bearer test-token";

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(expectedResponse, Encoding.UTF8, "application/json")
            });
        var context = new DefaultHttpContext();
        context.Request.Headers["Authorization"] = token;

        var client = _httpClient;
        
        //Act
        var result = await CompanyEndpointsHandlers.GetAllCompaniesHandler(client, context.Request, pageNumber);
        
        //Assert
        result.ShouldNotBeNull();
        var contentResult = result.ShouldBeOfType<ContentHttpResult>();
        contentResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        contentResult.ResponseContent.ShouldBe(expectedResponse);

    }
    

}