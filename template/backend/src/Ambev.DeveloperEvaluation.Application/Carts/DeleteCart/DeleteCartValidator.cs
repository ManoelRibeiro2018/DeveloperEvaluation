using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart
{
    public class DeleteCartValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .NotNull()
                .WithMessage("Id is required");
        }
    }
}
