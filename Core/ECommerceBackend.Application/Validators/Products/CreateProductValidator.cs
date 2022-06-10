using ECommerceBackend.Application.ViewModels.Products;
using FluentValidation;

namespace ECommerceBackend.Application.Validators.Products;

public class CreateProductValidator : AbstractValidator<CreateProductVm>
{
    public CreateProductValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
                .WithMessage("Name is required")
            .MaximumLength(150)
            .MinimumLength(3)
                .WithMessage("Name must be between 3 and 150 characters");

        RuleFor(p => p.Stock)
            .NotNull()
            .NotEmpty()
                .WithMessage("Stock is required")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Stock must be greater than or equal to 0");

        RuleFor(p => p.Price)
            .NotNull()
            .NotEmpty()
                .WithMessage("Price is required")
            .GreaterThanOrEqualTo(0)
                .WithMessage("Price must be greater than or equal to 0");
    }
}