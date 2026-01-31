using MediatR;
using CouponSystem.Application.DTOs;
using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Aggregates.Redemption;
using CouponSystem.Domain.Repositories;
using CouponSystem.Domain.Exceptions;

namespace CouponSystem.Application.Commands;

public class RedeemCouponCommandHandler : IRequestHandler<RedeemCouponCommand, RedemptionResultDto>
{
    private readonly ICouponRepository _couponRepository;
    private readonly IRedemptionRepository _redemptionRepository;
    
    public RedeemCouponCommandHandler(
        ICouponRepository couponRepository,
        IRedemptionRepository redemptionRepository)
    {
        _couponRepository = couponRepository;
        _redemptionRepository = redemptionRepository;
    }
    
    public async Task<RedemptionResultDto> Handle(RedeemCouponCommand request, CancellationToken cancellationToken)
    {
        var couponCode = new CouponCode(request.CouponCode);
        var coupon = await _couponRepository.GetByCodeAsync(couponCode, cancellationToken);
        
        if (coupon == null)
            throw new NotFoundException($"Coupon {request.CouponCode} not found");
        
        if (!coupon.IsActive || coupon.IsDeleted)
            throw new DomainException("Coupon is not active");
        
        if (!coupon.ValidityPeriod.IsActiveAt(DateTime.UtcNow))
            throw new DomainException("Coupon has expired");
        
        // Simplified validation - calculate discount
        var subtotal = request.CartItems.Sum(item => item.Price * item.Quantity);
        var discountAmount = coupon.Discount.CalculateDiscount(subtotal);
        discountAmount = Math.Min(discountAmount, coupon.MaxDiscountAmount);
        
        // Create reservation
        var redemption = Redemption.Reserve(coupon.Id, request.UserId, discountAmount);
        await _redemptionRepository.AddAsync(redemption, cancellationToken);
        
        coupon.IncrementUsageCount();
        await _couponRepository.UpdateAsync(coupon, cancellationToken);
        
        return new RedemptionResultDto
        {
            ReservationToken = redemption.ReservationToken!.Value,
            DiscountAmount = discountAmount,
            ExpiresAt = DateTime.UtcNow.AddMinutes(10),
            Message = "Coupon reserved successfully"
        };
    }
}
