using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers;

public interface ICoreModule
{
    void Load(IServiceCollection services);
}