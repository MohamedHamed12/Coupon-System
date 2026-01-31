using CouponSystem.Domain.Aggregates.Redemption;

namespace CouponSystem.Domain.Repositories;

public interface IRedemptionRepository
{
    Task<Redemption?> GetByIdAsync(RedemptionId id, CancellationToken cancellationToken = default);
    Task<Redemption?> GetByReservationTokenAsync(ReservationToken token, CancellationToken cancellationToken = default);
    Task<List<Redemption>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task AddAsync(Redemption redemption, CancellationToken cancellationToken = default);
    Task UpdateAsync(Redemption redemption, CancellationToken cancellationToken = default);
}
