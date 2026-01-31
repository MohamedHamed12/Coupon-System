using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Aggregates.Redemption;

namespace CouponSystem.Domain.Events;

public record CouponReservationCancelledEvent(CouponId CouponId, Guid UserId, ReservationToken Token) : DomainEvent;
