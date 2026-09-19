using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Volett.Actions;

namespace Volett.Features.Actions;

internal static class ActionRegistrar
{
    public static void Register(IServiceCollection services, ApplicationPartManager applicationParts)
    {
        var actionTypes = applicationParts.ApplicationParts
            .OfType<AssemblyPart>()
            .SelectMany(part => part.Assembly.DefinedTypes)
            .Where(type =>
                type is { IsClass: true, IsAbstract: false, ContainsGenericParameters: false }
                && type.ImplementedInterfaces.Any(contract =>
                    contract.IsGenericType
                    && contract.GetGenericTypeDefinition() == typeof(IAction<,>)));

        foreach (var actionType in actionTypes)
        {
            services.TryAddScoped(actionType.AsType());
        }
    }
}
