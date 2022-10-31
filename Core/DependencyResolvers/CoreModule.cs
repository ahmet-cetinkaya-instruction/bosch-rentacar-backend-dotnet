using System.Diagnostics;
using Core.Business.Mailing;
using Core.Business.Mailing.MailKit;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Logging.Serilog;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Core.DependencyResolvers;

public class CoreModule : ICoreModule
{
    public void Load(IServiceCollection services)
    {
        services.AddSingleton<IMailService, MailKitMailService>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<Stopwatch>();
        services.AddMemoryCache();
        services.AddSingleton<ICacheManager, MemoryCacheManager>();
        services.AddSingleton<FileLogger>();
        //services.AddSingleton<MongoDbLogger>();
    }
}