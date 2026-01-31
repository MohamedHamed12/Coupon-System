using Microsoft.EntityFrameworkCore;
using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Repositories;

namespace CouponSystem.Infrastructure.Persistence.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly CouponDbContext _context;
    
    public CouponRepository(CouponDbContext context)
    {
        _context = context;
    }
    
    public async Task<Coupon?> GetByIdAsync(CouponId id, CancellationToken cancellationToken = default)
    {
        return await _context.Coupons
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    
    public async Task<Coupon?> GetByCodeAsync(CouponCode code, CancellationToken cancellationToken = default)
    {
        return await _context.Coupons
            .FirstOrDefaultAsync(c => c.Code.Value == code.Value, cancellationToken);
    }
    
    public async Task<List<Coupon>> GetActiveCouponsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Coupons
            .Where(c => c.IsActive)
            .ToListAsync(cancellationToken);
    }
    
    public async Task AddAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        await _context.Coupons.AddAsync(coupon, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(Coupon coupon, CancellationToken cancellationToken = default)
    {
        _context.Coupons.Update(coupon);
        await _context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task DeleteAsync(CouponId id, CancellationToken cancellationToken = default)
    {
        var coupon = await GetByIdAsync(id, cancellationToken);
        if (coupon != null)
        {
            coupon.Delete();
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
