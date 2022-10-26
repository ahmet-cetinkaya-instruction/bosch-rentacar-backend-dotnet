using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Caching;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects;

// C-UD işlemlerinde çalışması gerekir.
public class CacheRemoveAspect : MethodInterception
{
    private readonly ICacheManager _cacheManager;
    private readonly string _pattern;

    public CacheRemoveAspect(string pattern)
    {
        _cacheManager = ServiceTool.ServiceProvider.GetService<ICacheManager>();
        _pattern = pattern;
    }

    protected override void OnSuccess(IInvocation invocation)
    {
        _cacheManager.RemoveByPattern(_pattern);
    }
}