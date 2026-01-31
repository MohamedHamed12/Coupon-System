using CouponSystem.Domain.Common;
using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Enums;
using CouponSystem.Domain.Events;

namespace CouponSystem.Domain.Aggregates.Redemption;

public class Redemption : AggregateRoot<RedemptionId>
{
    public CouponId CouponId { get; private set; } = null!;
    public Guid UserId { get; private set; }
    public Guid? OrderId { get; private set; }
    public decimal DiscountAmount { get; private set; }
    public RedemptionStatus Status { get; private set; }
    public ReservationToken? ReservationToken { get; private set; }
    public DateTime ReservedAt { get; private set; }
    public DateTime? ConfirmedAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    private Redemption() { } // EF Core
    
    public static Redemption Reserve(CouponId couponId, Guid userId, decimal discountAmount)
    {
        var redemption = new Redemption
        {
            Id = RedemptionId.New(),
            CouponId = couponId,
            UserId = userId,
            DiscountAmount = discountAmount,
            Status = RedemptionStatus.Reserved,
            ReservationToken = ReservationToken.Generate(),
            ReservedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        
        redemption.AddDomainEvent(new CouponReservedEvent(couponId, userId, redemption.ReservationToken));
        
        return redemption;
    }
    
    public void Confirm(Guid orderId)
    {
        if (Status != RedemptionStatus.Reserved)
            throw new InvalidOperationException("Can only confirm reserved redemptions");
            
        OrderId = orderId;
        Status = RedemptionStatus.Confirmed;
        ConfirmedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new CouponRedeemedEvent(CouponId, UserId, orderId, DiscountAmount));
    }
    
    public void Rollback()
    {
        if (Status != RedemptionStatus.Reserved)
            throw new InvalidOperationException("Can only rollback reserved redemptions");
            
        Status = RedemptionStatus.Cancelled;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new CouponReservationCancelledEvent(CouponId, UserId, ReservationToken!));
    }
}
