using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Domain.Events;

public record CouponCreatedEvent(CouponId CouponId, CouponCode Code) : DomainEvent;
