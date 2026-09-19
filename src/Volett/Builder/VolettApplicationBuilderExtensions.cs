using Microsoft.Extensions.DependencyInjection;
using Volett;

namespace Microsoft.AspNetCore.Builder;

/// <summary>
/// Adds Volett to an ASP.NET Core application's request pipeline.
/// </summary>
public static class VolettApplicationBuilderExtensions
{
    /// <summary>
    /// Configures the Volett request pipeline.
    /// </summary>
    public static IApplicationBuilder UseVolett(this IApplicationBuilder application)
    {
        ArgumentNullException.ThrowIfNull(application);

        if (application.ApplicationServices.GetService<VolettMarker>() is null)
        {
            throw new InvalidOperationException(
                "Volett services have not been registered. Call AddVolett() while configuring services.");
        }

        return application;
    }
}
