using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(e => e.Category)
                .NotEmpty()
                .NotNull()
                .WithMessage("Category is required");

            RuleFor(e => e.Price)
                .GreaterThan(0)
                .WithMessage("Pricce is required");

            RuleFor(e => e.Image)
                .NotEmpty()
                .NotNull()
                .WithMessage("Image is required");
        }
    }
}
