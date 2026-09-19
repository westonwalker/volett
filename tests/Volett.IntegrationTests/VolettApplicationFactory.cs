using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Volett.IntegrationTests;

public sealed class VolettApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services
                .AddControllers()
                .PartManager
                .ApplicationParts
                .Add(new AssemblyPart(typeof(TestingController).Assembly));
        });
    }
}

[Route("testing")]
public sealed class TestingController : ControllerBase
{
    [HttpGet("failure")]
    public IActionResult Failure()
    {
        throw new InvalidOperationException("This exception should become a problem response.");
    }
}
