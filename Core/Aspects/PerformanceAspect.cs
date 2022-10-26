using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects;

public class PerformanceAspect : MethodInterception
{
    private readonly int _interval;
    private readonly Stopwatch _stopwatch; // sayaç, kronometre

    public PerformanceAspect(int interval) // 2sn // İlgili metotun beklendiği süre içerisinde işlemini işlemin gerçekleştirmesi
    {
        _interval = interval;
        _stopwatch = ServiceTool.ServiceProvider.GetService<Stopwatch>()!;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        // İlgili metot çalışmadan önce sayaç başlatmamız gerekiyor.
        _stopwatch.Start(); // sayaç başlatır
    }

    protected override void OnAfter(IInvocation invocation)
    {
        if(_stopwatch.Elapsed.TotalSeconds // o ana kadar geçen süreyi verir
           > _interval) // parametreyle hedef süremizdi
        // "Performance : BrandManager.GetList - 5/2" şeklinde debug konsolundan geliştiricilere haberdar ettik.
            Debug.WriteLine(
                $"Performance : {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name} ---> {_stopwatch.Elapsed.TotalSeconds} / {_interval}");

        // Sayacın saymaya devam etmemesi için, sayacı 0'a eşitleyip durdurduk.
        _stopwatch.Reset();
    }
}