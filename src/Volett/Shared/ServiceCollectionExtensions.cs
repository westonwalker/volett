using Microsoft.Extensions.DependencyInjection.Extensions;
using Volett.Features.Actions;
using Volett.Shared;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Registers Volett's application services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Volett to an ASP.NET Core application's service collection.
    /// </summary>
    public static IServiceCollection AddVolett(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<RegistrationMarker>();
        services.AddProblemDetails();

        var mvc = services.AddControllers();

        ActionRegistrar.Register(services, mvc.PartManager);

        return services;
    }
}
