using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
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
        //yapılan örnek için: new CreateBrandRequestValidator()

        //+todo: _validatorType base sınıfına bakacağız. base sınıfın jenerik argümanlanını alıcaz.
        Type typeToValidate = _validatorType.BaseType.GetGenericArguments()[0];


        //+todo: ilgili metodun parametelerine bakacağız. sadece validate edeceğimiz parameterleri alıcaz
        IEnumerable<object> argumentsToValidate = invocation.Arguments.Where(o => o.GetType() == typeToValidate);

        
        //+todo: parameterleri tek tek validate edicez.
        foreach (object argumentToValidate in argumentsToValidate)
        {
            ValidationTool.Validate(validator, argumentToValidate);
        }
    }
}