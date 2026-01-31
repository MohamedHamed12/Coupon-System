using Microsoft.EntityFrameworkCore;
using CouponSystem.Domain.Aggregates.Redemption;
using CouponSystem.Domain.Repositories;

namespace CouponSystem.Infrastructure.Persistence.Repositories;

public class RedemptionRepository : IRedemptionRepository
{
    private readonly CouponDbContext _context;
    
    public RedemptionRepository(CouponDbContext context)
    {
        _context = context;
    }
    
    public async Task<Redemption?> GetByIdAsync(RedemptionId id, CancellationToken cancellationToken = default)
    {
        return await _context.Redemptions
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
    }
    
    public async Task<Redemption?> GetByReservationTokenAsync(ReservationToken token, CancellationToken cancellationToken = default)
    {
        return await _context.Redemptions
            .FirstOrDefaultAsync(r => r.ReservationToken!.Value == token.Value, cancellationToken);
    }
    
    public async Task<List<Redemption>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Redemptions
            .Where(r => r.UserId == userId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);
    }
    
    public async Task AddAsync(Redemption redemption, CancellationToken cancellationToken = default)
    {
        await _context.Redemptions.AddAsync(redemption, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Redemption redemption, CancellationToken cancellationToken = default)
    {
        _context.Redemptions.Update(redemption);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
