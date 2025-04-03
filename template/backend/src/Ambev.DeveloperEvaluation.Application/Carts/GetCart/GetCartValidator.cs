using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart
{
    public class GetCartValidator : AbstractValidator<GetCartCommand>
    {
        public GetCartValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id is required");
        }
    }
}
