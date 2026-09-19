using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volett.Database;
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

        services.AddOptions<DatabaseOptions>();
        AddVolettCore(services);

        return services;
    }

    /// <summary>
    /// Adds Volett using the application's configuration. The database driver must be set to
    /// <c>none</c> when no EF Core context is supplied.
    /// </summary>
    public static IServiceCollection AddVolett(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var database = DatabaseRegistrar.RegisterOptions(services, configuration);
        DatabaseRegistrar.EnsureDatabaseIsDisabled(database);
        AddVolettCore(services);

        return services;
    }

    /// <summary>
    /// Adds Volett and configures the application's EF Core context from
    /// <c>Volett:Database</c> in application configuration.
    /// </summary>
    public static IServiceCollection AddVolett<TDbContext>(
        this IServiceCollection services,
        IConfiguration configuration)
        where TDbContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var database = DatabaseRegistrar.RegisterOptions(services, configuration);
        DatabaseRegistrar.RegisterContext<TDbContext>(services, database);
        AddVolettCore(services);

        return services;
    }

    private static void AddVolettCore(IServiceCollection services)
    {
        services.TryAddSingleton<RegistrationMarker>();
        services.AddProblemDetails();

        var mvc = services.AddControllers();

        ActionRegistrar.Register(services, mvc.PartManager);
    }
}
