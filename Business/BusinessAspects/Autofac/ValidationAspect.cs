using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using FluentValidation;

namespace Business.BusinessAspects.Autofac;

public class ValidationAspect : MethodInterception
{
    private readonly Type _validatorType;
    public ValidationAspect(Type validatorType)
    {
        if (!typeof(IValidator).IsAssignableFrom(validatorType))
            throw new ArgumentException("Wrong validator type.");

        _validatorType = validatorType;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        // Reflection kullanarak _validatorType'ın tuttuğu tipte instance oluşturduk, yani new'ledik.
        // cast ve as farkı, cast casting işlemi başarısız olduğunda hata fırlatıyor. as ise null değer dönüyor.
        IValidator validator = (Activator.CreateInstance(_validatorType) as IValidator)!;

        //todo: kontrol edeceğimiz nesne, request'i alıcaz. Parametreerini okuyarak alıcaz.

        //todo: _validatorType base sınıfına bakacağız. base sınıfın jenerik argümanlanını alıcaz.
        //todo: ilgili metodun parametelerine bakacağız. sadece validate edeceğimiz parameterleri alıcaz

        //todo: parameterleri tek tek validate edicez.
    }
}