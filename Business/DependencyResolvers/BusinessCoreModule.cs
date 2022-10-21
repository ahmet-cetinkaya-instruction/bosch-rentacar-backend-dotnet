using Core.DependencyResolvers;
using Microsoft.Extensions.DependencyInjection;

namespace Business.DependencyResolvers;

public class BusinessCoreModule : ICoreModule
{
    public void Load(IServiceCollection services)
    {
        services.AddAutoMapper(assemblies: AppDomain.CurrentDomain.GetAssemblies());
    }
}