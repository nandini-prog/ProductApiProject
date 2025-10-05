using Application.DTOs;
using FluentValidation;

namespace Application.Validators
{
    /// <summary>
    /// Validator for UpdateProductDto - ensures product updates are valid.
    /// </summary>
    public class UpdateProductDtoValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductDtoValidator()
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product name is required.")
                .MinimumLength(3).WithMessage("Product name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");
        }
    }
}
