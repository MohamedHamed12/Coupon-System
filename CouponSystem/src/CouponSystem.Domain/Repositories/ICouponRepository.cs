using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Domain.Repositories;

public interface ICouponRepository
{
    Task<Coupon?> GetByIdAsync(CouponId id, CancellationToken cancellationToken = default);
    Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken = default);
    Task<List<Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default);
    Task AddAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default);
    Task DeleteAsync(CouponId id, CancellationToken cancellationToken = default);
}
