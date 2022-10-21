using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

public abstract class MethodInterception : MethodInterceptionBaseAttribute
{
    protected virtual void OnBefore(IInvocation invocation){}

    public override void Intercept(IInvocation invocation) // method
    {
        OnBefore(invocation);
        invocation.Proceed(); // Method burada çalıştırılıyor.
    }
}