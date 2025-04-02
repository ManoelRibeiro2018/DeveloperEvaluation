using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProductsByCategory
{
    public class GetAllPRoductsByCategoryValidator : AbstractValidator<GetAllProductsByCategoryCommand>
    {
        public GetAllPRoductsByCategoryValidator()
        {
            RuleFor(e => e.Category)
                .NotEmpty()
                .NotNull()
                .WithMessage("Category is required");
        }
    }
}
