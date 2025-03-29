using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator()
        {
            RuleFor(e => e.SaleItens)
                .NotEmpty()
                .WithMessage("Produt is required");

            RuleFor(e => e.BranchId)
                .NotEqual(default(Guid))
                .NotNull()
                .WithMessage("BranchId is required");

            RuleFor(e => e.UserId)
               .NotEqual(default(Guid))
               .NotNull()
               .WithMessage("UserId is required");
        }
    }
}
