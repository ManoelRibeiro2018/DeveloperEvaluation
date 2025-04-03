using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CreateCartValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartValidator()
        {
            RuleFor(x => x.UserId)
             .NotEmpty().WithMessage("UserId is required.");

            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Date is required");

            RuleFor(x => x.Products)
                .NotEmpty().WithMessage("The cart must have at least one item.")
                .ForEach(item =>
                    item.SetValidator(new CartItemValidator())
                );
        }
    }
}
