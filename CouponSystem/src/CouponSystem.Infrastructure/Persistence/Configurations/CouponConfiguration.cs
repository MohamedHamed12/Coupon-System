using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Infrastructure.Persistence.Configurations;

public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
{
    public void Configure(EntityTypeBuilder<Coupon> builder)
    {
        builder.ToTable("coupons");
        
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion(id => id.Value, value => CouponId.From(value))
            .HasColumnName("id");
        
        builder.OwnsOne(c => c.Code, code =>
        {
            code.Property(c => c.Value)
                .HasColumnName("code")
                .HasMaxLength(50)
                .IsRequired();
        });
        
        builder.OwnsOne(c => c.Discount, discount =>
        {
            discount.Property(d => d.Type).HasColumnName("discount_type");
            discount.Property(d => d.Value).HasColumnName("discount_value").HasColumnType("decimal(18,2)");
        });
        
        builder.OwnsOne(c => c.Limits, limits =>
        {
            limits.Property(l => l.MaxTotalUses).HasColumnName("max_total_uses");
            limits.Property(l => l.MaxUsesPerUser).HasColumnName("max_uses_per_user");
            limits.Property(l => l.MaxUsesPerDay).HasColumnName("max_uses_per_day");
        });
        
        builder.OwnsOne(c => c.ValidityPeriod, period =>
        {
            period.Property(p => p.StartDate).HasColumnName("start_date");
            period.Property(p => p.EndDate).HasColumnName("end_date");
        });
        
        builder.Property(c => c.Status).HasColumnName("status");
        builder.Property(c => c.IsActive).HasColumnName("is_active");
        builder.Property(c => c.MaxDiscountAmount).HasColumnName("max_discount_amount").HasColumnType("decimal(18,2)");
        builder.Property(c => c.CurrentUses).HasColumnName("current_uses");
        builder.Property(c => c.CreatedAt).HasColumnName("created_at");
        builder.Property(c => c.UpdatedAt).HasColumnName("updated_at");
        builder.Property(c => c.IsDeleted).HasColumnName("is_deleted");
        
        builder.HasIndex(c => c.Code.Value).IsUnique();
        builder.HasQueryFilter(c => !c.IsDeleted);
        
        builder.Ignore(c => c.DomainEvents);
    }
}
