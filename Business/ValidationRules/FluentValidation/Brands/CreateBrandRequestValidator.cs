using Business.Requests.Brands;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Brands;

public class CreateBrandRequestValidator : AbstractValidator<CreateBrandRequest>
{
    public CreateBrandRequestValidator()
    {
        // Fluent Yapı
        RuleFor(r => r.Name)
            .NotEmpty()
            .MinimumLength(2)
            .Must(StartWithB).WithMessage("Brand name should be start with 'B'.");

        // Matches ile Regex'i kullanabiliriz.
    }


    private bool StartWithB(string value)
    {
        return value.StartsWith("B");
    }
}