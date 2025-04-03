using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.UpdateCart
{
    public class UpdateCartValidator : AbstractValidator<UpdateCartCommand>
    {
        public UpdateCartValidator() {

            RuleFor( x => x.Id )
                .NotEmpty()
                .WithMessage("Id is required");

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
