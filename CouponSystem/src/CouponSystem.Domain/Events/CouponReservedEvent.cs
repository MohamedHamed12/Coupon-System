using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Aggregates.Redemption;

namespace CouponSystem.Domain.Events;

public record CouponReservedEvent(CouponId CouponId, Guid UserId, ReservationToken Token) : DomainEvent;
