using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Volett.IntegrationTests;

public sealed class HttpKernelTests(VolettApplicationFactory application)
    : IClassFixture<VolettApplicationFactory>
{
    [Fact]
    public async Task Valid_request_runs_action_and_returns_created_response()
    {
        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/todos", new
        {
            Title = "Ship Volett"
        });

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.NotNull(response.Headers.Location);

        using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

        Assert.Equal("Ship Volett", body.RootElement.GetProperty("title").GetString());
        Assert.NotEqual(Guid.Empty, body.RootElement.GetProperty("id").GetGuid());
    }

    [Fact]
    public async Task Invalid_request_returns_validation_problem_details()
    {
        var client = application.CreateClient();

        var response = await client.PostAsJsonAsync("/todos", new
        {
            Title = string.Empty
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>();

        Assert.NotNull(problem);
        Assert.Contains("Title", problem.Errors.Keys);
        Assert.True(problem.Extensions.ContainsKey("traceId"));
    }

    [Fact]
    public async Task Unhandled_exception_returns_problem_details()
    {
        var client = application.CreateClient();

        var response = await client.GetAsync("/testing/failure");

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal(StatusCodes.Status500InternalServerError, problem.Status);
        Assert.True(problem.Extensions.ContainsKey("traceId"));
    }

    [Fact]
    public async Task Missing_endpoint_returns_problem_details()
    {
        var client = application.CreateClient();

        var response = await client.GetAsync("/missing");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);

        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();

        Assert.NotNull(problem);
        Assert.Equal(StatusCodes.Status404NotFound, problem.Status);
        Assert.True(problem.Extensions.ContainsKey("traceId"));
    }

    [Fact]
    public void Controller_endpoint_contains_aspnet_metadata()
    {
        _ = application.CreateClient();

        var endpointDataSource = application.Services.GetRequiredService<EndpointDataSource>();
        var endpoint = endpointDataSource.Endpoints
            .OfType<RouteEndpoint>()
            .Single(item => item.RoutePattern.RawText == "todos");
        var metadata = endpoint.Metadata.GetRequiredMetadata<ControllerActionDescriptor>();

        Assert.Equal("Todos", metadata.ControllerName);
        Assert.Equal("Store", metadata.ActionName);
    }
}
