using System;
using FluentValidation;
using CouponSystem.Application.Commands;

namespace CouponSystem.Application.Validators;

public class RedeemCouponCommandValidator : AbstractValidator<RedeemCouponCommand>
{
    public RedeemCouponCommandValidator()
    {
        RuleFor(x => x.CouponCode).NotEmpty();
        RuleFor(x => x.UserId).NotEqual(Guid.Empty);
        RuleFor(x => x.CartItems).NotEmpty();
        RuleForEach(x => x.CartItems).SetValidator(new CartItemDtoValidator());
    }
}
