using System.Net;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using QApiGateway.DTO.AuthDTO;
using QApiGateway.EndpointHandlers;
using QApiGateway.Endpoints;
using Shouldly;

namespace QApiGateway.UnitTest.EndpointTests.AuthEndpointTests;

public class ResendCodeTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly WebApplication _app;

    public ResendCodeTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(_httpClient);
        _app = builder.Build();
        _app.MapCustomerEndpoints();
    }

    [Fact]
    public async Task Resend_Code_Should_Return_New_Code()
    {
        // Arrange
        var request = new ResendCodeRequest
        {
            EmailAddress = "test@gmail.com"
        };
        var expectedResponse = "{Code sent}";
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
        var result = await AuthEndpointHandlers.ResendCodeHandler(
            client,
            request);

        // Assert
        result.ShouldNotBeNull();
        var contentResult = result.ShouldBeOfType<ContentHttpResult>();
        contentResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        contentResult.ResponseContent.ShouldBe(expectedResponse);
    }
}