using Microsoft.Extensions.DependencyInjection;
using Volett.Shared;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Adds Volett to an ASP.NET Core application's request pipeline.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Configures Volett's middleware and maps its HTTP endpoints.
    /// </summary>
    public static WebApplication UseVolett(this WebApplication application)
    {
        ArgumentNullException.ThrowIfNull(application);

        if (application.Services.GetService<RegistrationMarker>() is null)
        {
            throw new InvalidOperationException(
                "Volett services have not been registered. Call AddVolett() while configuring services.");
        }

        application.UseExceptionHandler();
        application.UseStatusCodePages();
        application.MapControllers();

        return application;
    }
}
