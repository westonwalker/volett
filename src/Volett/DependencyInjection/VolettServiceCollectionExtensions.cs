using Microsoft.Extensions.DependencyInjection.Extensions;
using Volett;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Registers Volett's application services.
/// </summary>
public static class VolettServiceCollectionExtensions
{
    /// <summary>
    /// Adds Volett to an ASP.NET Core application's service collection.
    /// </summary>
    public static IServiceCollection AddVolett(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.TryAddSingleton<VolettMarker>();

        return services;
    }
}
