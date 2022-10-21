using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors;

public abstract class MethodInterception : MethodInterceptionBaseAttribute
{
    // Cross Cutting Concerns işlemlerinde kullanacağımız event metotları:
    protected virtual void OnBefore(IInvocation invocation){}
    protected virtual void OnAfter(IInvocation invocation){}
    protected virtual void OnSuccess(IInvocation invocation){}
    protected virtual void OnException(IInvocation invocation, Exception exception){}

    // aspect uygulanan metot çalışmadan önce aşağıdaki metodumuz araya girecektir.
    public override void Intercept(IInvocation invocation) // method
    {
        bool isSuccess = true;
        OnBefore(invocation);
        try
        {
            invocation.Proceed(); // Method çalıştırılıyor.
        }
        catch (Exception e)
        {
            isSuccess = false;
            OnException(invocation, e);
            throw;
        }
        finally
        {
            if(isSuccess) OnSuccess(invocation);
        }
        OnAfter(invocation);
    }
}