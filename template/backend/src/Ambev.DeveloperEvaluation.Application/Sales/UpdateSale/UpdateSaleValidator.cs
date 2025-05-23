﻿using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(e => e.Id)
                .NotEmpty()
                .NotNull()
                .NotEqual(default(Guid))
                .WithMessage("It's necessary valid Id");

            RuleFor(e => e.BranchId)
                .NotEmpty()
                .NotNull()
                .NotEqual(default(Guid))
                .WithMessage("It's necessary valid BranchId");

            RuleFor(e => e.SaleItens)
                .NotEmpty()
                .WithMessage("It's necessary valid list item sale");
        }
    }
}
