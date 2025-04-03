using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.CreateCart
{
    public class CartItemValidator : AbstractValidator<CartItem>
    {
        public CartItemValidator()
        {
            RuleFor(e => e.ProductId)
                .NotEmpty()
                .WithMessage("ProductId is required");

            RuleFor(e => e.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity is required");
        }
    }
}
