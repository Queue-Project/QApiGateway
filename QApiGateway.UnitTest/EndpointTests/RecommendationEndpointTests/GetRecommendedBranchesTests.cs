using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using QApiGateway.DTO.RecommendationDTO;
using QApiGateway.DTO.ReportDTO;
using QApiGateway.EndpointHandlers;
using QApiGateway.Endpoints;
using Shouldly;

namespace QApiGateway.UnitTest.EndpointTests.RecommendationEndpointTests;

public class GetRecommendedBranchesTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly WebApplication _app;

    public GetRecommendedBranchesTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(_httpClient);
        _app = builder.Build();
        _app.MapRecommendationEndpoints();
    }

    [Fact]
    public async Task GetRecommendedBranches_Should_Return_RecommendedBranches_When_Authorized()
    {
        var request = new RecommendedBranchRequest
        {
            CompanyId = 1,
            PageNumber = 1,
        };

        // Arrange
        var expectedResponse = "{Items: BranchId= 1, BranchName = TestName}";
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
        var result = await RecommendationEndpointHandler.GetRecommendedBranches(
            client,
            context.Request, request);

        // Assert
        result.ShouldNotBeNull();
        var contentResult = result.ShouldBeOfType<ContentHttpResult>();
        contentResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        contentResult.ResponseContent.ShouldBe(expectedResponse);
    }
}