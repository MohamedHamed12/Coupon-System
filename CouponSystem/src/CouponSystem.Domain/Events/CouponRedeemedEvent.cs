using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Domain.Events;

public record CouponRedeemedEvent(CouponId CouponId, Guid UserId, Guid OrderId, decimal DiscountAmount) : DomainEvent;
