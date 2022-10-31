using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects;

public class LogAspect : MethodInterception
{
    private readonly ILoggingManager _loggingManager;

    public LogAspect(Type loggingManagerType)
    {
        if (!typeof(ILoggingManager).IsAssignableFrom(loggingManagerType))
            throw new ArgumentException("Wrong logging manager type.");

        _loggingManager = ServiceTool.ServiceProvider.GetService<ILoggingManager>()!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        _loggingManager.Info(GetLogDetail(invocation));
    }

    private string GetLogDetail(IInvocation invocation)
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
            MethodName = invocation.Method.Name,
            Parameters = logParameters
            //todo: User
        };

        //todo: return json string
        return String.Empty;
    }

    //todo: GetLogDetailException
}

