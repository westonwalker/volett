using Microsoft.Extensions.DependencyInjection;
using Volett.Example.Actions;

namespace Volett.IntegrationTests.Features.Actions;

public sealed class ActionRegistrationTests(VolettApplicationFactory application)
    : IClassFixture<VolettApplicationFactory>
{
    [Fact]
    public void Actions_are_registered_with_scoped_lifetime()
    {
        using var firstScope = application.Services.CreateScope();
        using var secondScope = application.Services.CreateScope();

        var first = firstScope.ServiceProvider.GetRequiredService<CreateTodo>();
        var sameScope = firstScope.ServiceProvider.GetRequiredService<CreateTodo>();
        var differentScope = secondScope.ServiceProvider.GetRequiredService<CreateTodo>();

        Assert.Same(first, sameScope);
        Assert.NotSame(first, differentScope);
    }
}
