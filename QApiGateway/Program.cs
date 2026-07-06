using QApiGateway.Endpoints;
using QApiGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGatewayServices(builder.Configuration);

builder.Services.AddHttpClient("BranchService",
    client => { client.BaseAddress = new Uri("http://branch-service:5002/"); });


builder.Services.AddHttpClient("UserService", client => { client.BaseAddress = new Uri("http://user-service:5004/"); });

builder.Services.AddHttpClient("QueueService",
    client => { client.BaseAddress = new Uri("http://queue-service:5006/"); });

builder.Services.AddHttpClient("AggregationService",
    client => { client.BaseAddress = new Uri("http://aggregation-service:5008/"); });

builder.Services.AddHttpClient("QSearchService",
    client => { client.BaseAddress = new Uri("http://search-service:5088/"); });


var app = builder.Build();

app.UseGatewayPipeline();

app.MapAuthEndpoints();
app.MapQueueEndpoints();
app.MapReviewEndpoints();
app.MapComplaintEndpoints();
app.MapReportEndpoints();
app.MapCompanyEndpoints();
app.MapEmployeeInfoEndpoints();
app.MapCustomerEndpoints();
app.MapFavoriteEmployeesEndpoints();
app.MapSearchEndpoints();
app.Run();