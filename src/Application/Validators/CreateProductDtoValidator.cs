using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for CreateProductDto - ensures new product data is valid.
    /// </summary>
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        }
    }
}
