using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Rating.Rate).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Rating.Count).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).MaximumLength(500);
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
