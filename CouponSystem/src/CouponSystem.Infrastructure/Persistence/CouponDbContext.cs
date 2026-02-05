using Microsoft.EntityFrameworkCore;
using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Aggregates.Redemption;
using CouponSystem.Domain.Aggregates.User;

namespace CouponSystem.Infrastructure.Persistence;

public class CouponDbContext : DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = null!;
    public DbSet<Redemption> Redemptions { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    
    public CouponDbContext(DbContextOptions<CouponDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CouponDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
