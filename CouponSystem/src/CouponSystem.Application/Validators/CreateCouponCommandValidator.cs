using System;
using FluentValidation;
using CouponSystem.Application.Commands;
using CouponSystem.Domain.Enums;

namespace CouponSystem.Application.Validators;

public class CreateCouponCommandValidator : AbstractValidator<CreateCouponCommand>
{
    public CreateCouponCommandValidator()
    {
        RuleFor(x => x.Code)
            .MaximumLength(50)
            .When(x => !string.IsNullOrWhiteSpace(x.Code));

        RuleFor(x => x.DiscountType)
            .NotEmpty()
            .Must(BeValidDiscountType).WithMessage("Invalid discount type.");

        RuleFor(x => x.DiscountValue)
            .GreaterThan(0).WithMessage("Discount value must be greater than 0.");

        When(x => Enum.TryParse<DiscountType>(x.DiscountType, true, out var dt) && dt == DiscountType.Percentage, () =>
        {
            RuleFor(x => x.DiscountValue)
                .LessThanOrEqualTo(100).WithMessage("Percentage discount must be less than or equal to 100.");
        });

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("StartDate must be earlier than EndDate.");

        RuleFor(x => x.MaxTotalUses)
            .GreaterThan(0).When(x => x.MaxTotalUses.HasValue);

        RuleFor(x => x.MaxUsesPerUser)
            .GreaterThan(0).When(x => x.MaxUsesPerUser.HasValue);

        RuleFor(x => x.MaxDiscountAmount)
            .GreaterThanOrEqualTo(0);
    }

    private bool BeValidDiscountType(string discountType)
    {
        return Enum.TryParse<DiscountType>(discountType, true, out _);
    }
}
