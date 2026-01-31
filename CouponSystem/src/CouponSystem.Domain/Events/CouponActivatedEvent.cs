using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Domain.Events;

public record CouponActivatedEvent(CouponId CouponId) : DomainEvent;
