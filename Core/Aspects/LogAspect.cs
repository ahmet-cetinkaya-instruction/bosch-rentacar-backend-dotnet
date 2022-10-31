using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Core.Aspects;

public class LogAspect : MethodInterception
{
    private readonly ILoggingManager _loggingManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogAspect(Type loggingManagerType)
    {
        if (!typeof(ILoggingManager).IsAssignableFrom(loggingManagerType))
            throw new ArgumentException("Wrong logging manager type.");

        _loggingManager = (ServiceTool.ServiceProvider.GetService(loggingManagerType) as ILoggingManager)!;
        _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>()!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _loggingManager.Info(getLogDetail(invocation).ToJson());
    }

    protected override void OnException(IInvocation invocation, Exception exception)
    {
        _loggingManager.Error(getLogDetailWithException(invocation, exception).ToJson());
    }

    private LogDetail getLogDetail(IInvocation invocation)
    {
        List<LogParameter> logParameters = new();
        for (int i = 0; i < invocation.Arguments.Length; ++i) // i++
        {
            logParameters.Add(new LogParameter
            {
                Name = invocation.GetConcreteMethod().GetParameters()[i].Name!,
                Type = invocation.Arguments[i].GetType().Name,
                Value = invocation.Arguments[i]
            });
        }


        LogDetail logDetail = new()
        {
            FullName = $"{invocation.Method.DeclaringType!.FullName}.{invocation.Method.Name}",
            MethodName = invocation.Method.Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext.User.Identity?.Name ?? "?"
        };

        return logDetail;
    }

    // Dry - Don't repeat yourself
    private LogDetailWithException getLogDetailWithException(IInvocation invocation, Exception exception)
    {
        LogDetail logDetail = getLogDetail(invocation);

        LogDetailWithException logDetailWithException = new()
        {
            FullName = logDetail.FullName,
            MethodName = logDetail.MethodName,
            Parameters = logDetail.Parameters,
            User = logDetail.User,
            ExceptionMessage = exception.Message
        };

        return logDetailWithException;
    }
}

