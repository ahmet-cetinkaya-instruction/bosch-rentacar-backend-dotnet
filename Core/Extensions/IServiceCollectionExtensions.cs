using Core.DependencyResolvers;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Extensions;

public static class IServiceCollectionExtensions
{

    public static IServiceCollection AddDependencyResolvers(this IServiceCollection services, ICoreModule[] modules)
    {
        foreach (ICoreModule module in modules)
        {
            module.Load(services);
        }
        
        return ServiceTool.Create(services);
    }
}