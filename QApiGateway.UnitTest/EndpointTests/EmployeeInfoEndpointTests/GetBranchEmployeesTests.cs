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

namespace QApiGateway.UnitTest.EndpointTests.EmployeeInfoEndpointTests;

public class GetBranchEmployeesTests
{
    private readonly Mock<HttpMessageHandler> _mockHttpMessageHandler;
    private readonly HttpClient _httpClient;
    private readonly WebApplication _app;

    public GetBranchEmployeesTests()
    {
        _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        _httpClient = new HttpClient(_mockHttpMessageHandler.Object);

        var builder = WebApplication.CreateBuilder();
        builder.Services.AddSingleton(_httpClient);
        _app = builder.Build();
        _app.MapEmployeeInfoEndpoints();
    }

    [Fact]
    public async Task GetBranchEmployees_Should_Return_Employees_When_Authorized()
    {
        // Arrange
        var companyId = 1;
        var branchId = 2;
        var pageNumber = 1;
        var expectedResponse = "{employees: id:1, name: Test Name}";
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
        context.Request.QueryString =
            new QueryString($"?companyId={companyId}&branchId={branchId}&pageNumber={pageNumber}");

        var client = _httpClient;

        // Act
        var result = await EmployeeInfoEndpointHandlers.GetBranchEmployeesHandler(
            client,
            context.Request,
            companyId,
            branchId,
            pageNumber);

        // Assert
        result.ShouldNotBeNull();
        var contentResult = result.ShouldBeOfType<ContentHttpResult>();
        contentResult.StatusCode.ShouldBe((int)HttpStatusCode.OK);
        contentResult.ResponseContent.ShouldBe(expectedResponse);
    }

    [Fact]
    public async Task GetBranchEmployees_Should_Return_Error_When_Service_Not_Found()
    {
        // Arrange
        var companyId = 1;
        var branchId = 999;
        var pageNumber = 1;
        var expectedResponse = "{error: Branch not found}";
        var token = "Bearer invalid-token";

        _mockHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(expectedResponse, Encoding.UTF8, "application/json")
            });

        var context = new DefaultHttpContext();
        context.Request.Headers["Authorization"] = token;

        var client = _httpClient;

        // Act
        var result = await EmployeeInfoEndpointHandlers.GetBranchEmployeesHandler(
            client,
            context.Request,
            companyId,
            branchId,
            pageNumber);

        // Assert
        result.ShouldNotBeNull();
        var contentResult = result.ShouldBeOfType<ContentHttpResult>();
        contentResult.StatusCode.ShouldBe((int)HttpStatusCode.NotFound);
        contentResult.ResponseContent.ShouldBe(expectedResponse);
    }
}