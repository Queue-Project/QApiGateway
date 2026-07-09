using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using QApiGateway.DTO;
using QApiGateway.DTO.ReportDTO;
using QApiGateway.EndpointHandlers;
using QApiGateway.Endpoints;
using Shouldly;

namespace QApiGateway.UnitTest.EndpointTests.SearchEndpointTests;

public class ElasticSearchTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly WebApplication _app;

    public ElasticSearchTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(_httpClient);
        _app = builder.Build();
        _app.MapSearchEndpoints();
    }

    [Fact]
    public async Task ElasticSearchTests_Should_Return_Result_When_Authorized()
    {

        var request = new SearchRequest()
        {
            SearchTerm = "Ali",
            PageNumber = 1,
            PageSize = 10,
        };
        
        // Arrange
        var expectedResponse = "{items: [entityId= 1, entityType = 2, title: Ali, Subtitle: +992945776788]}";
        var token = "Bearer test-token";

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
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

        // Act
        var result = await SearchEndpointHandlers.ElasticSearch(
            client,
            context.Request, request);

        // Assert
        result.ShouldNotBeNull();
        var contentResult = result.ShouldBeOfType<ContentHttpResult>();
        contentResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        contentResult.ResponseContent.ShouldBe(expectedResponse);
    }

}