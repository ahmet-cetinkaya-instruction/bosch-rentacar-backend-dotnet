using FluentValidation;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace Core.CrossCuttingConcerns.Validation.FluentValidation
{
    public static class ValidationTool
    {
        public static void Validate(IValidator validator, object obj)
        {
            ValidationContext<object> context = new(obj);
            ValidationResult result = validator.Validate(context);
            if (!result.IsValid)
                throw new ValidationException(result.Errors);
        }
    }
}