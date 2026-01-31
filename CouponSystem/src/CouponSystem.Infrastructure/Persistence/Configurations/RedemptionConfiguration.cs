using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CouponSystem.Domain.Aggregates.Redemption;
using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Infrastructure.Persistence.Configurations;

public class RedemptionConfiguration : IEntityTypeConfiguration<Redemption>
{
    public void Configure(EntityTypeBuilder<Redemption> builder)
    {
        builder.ToTable("redemptions");
        
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .HasConversion(id => id.Value, value => RedemptionId.From(value))
            .HasColumnName("id");
        
        builder.Property(r => r.CouponId)
            .HasConversion(id => id.Value, value => CouponId.From(value))
            .HasColumnName("coupon_id");
        
        builder.Property(r => r.UserId).HasColumnName("user_id");
        builder.Property(r => r.OrderId).HasColumnName("order_id");
        builder.Property(r => r.DiscountAmount).HasColumnName("discount_amount").HasColumnType("decimal(18,2)");
        builder.Property(r => r.Status).HasColumnName("status");
        
        builder.OwnsOne(r => r.ReservationToken, token =>
        {
            token.Property(t => t.Value)
                .HasColumnName("reservation_token")
                .HasMaxLength(100);
        });
        
        builder.Property(r => r.ReservedAt).HasColumnName("reserved_at");
        builder.Property(r => r.ConfirmedAt).HasColumnName("confirmed_at");
        builder.Property(r => r.CreatedAt).HasColumnName("created_at");
        builder.Property(r => r.UpdatedAt).HasColumnName("updated_at");
        
        builder.Ignore(r => r.DomainEvents);
    }
}
