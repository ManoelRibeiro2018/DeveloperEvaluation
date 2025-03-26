using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(default(Guid))
                .WithMessage("It's necessary valid value");
        }
    }
}
