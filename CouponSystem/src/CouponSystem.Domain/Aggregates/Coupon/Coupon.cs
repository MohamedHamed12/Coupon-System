using CouponSystem.Domain.Common;
using CouponSystem.Domain.Enums;
using CouponSystem.Domain.Events;

namespace CouponSystem.Domain.Aggregates.Coupon;

public class Coupon : AggregateRoot<CouponId>
{
    public CouponCode Code { get; private set; } = null!;
    public DiscountConfiguration Discount { get; private set; } = null!;
    public UsageLimits Limits { get; private set; } = null!;
    public DateRange ValidityPeriod { get; private set; } = null!;
    public CouponStatus Status { get; private set; }
    public bool IsActive { get; private set; }
    public decimal MaxDiscountAmount { get; private set; }
    public int CurrentUses { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public bool IsDeleted { get; private set; }
    
    private Coupon() { } // EF Core
    
    public static Coupon Create(
        CouponCode code,
        DiscountConfiguration discount,
        UsageLimits limits,
        DateRange validityPeriod,
        decimal maxDiscountAmount = 999999.99m)
    {
        var coupon = new Coupon
        {
            Id = CouponId.New(),
            Code = code,
            Discount = discount,
            Limits = limits,
            ValidityPeriod = validityPeriod,
            Status = CouponStatus.Draft,
            IsActive = false,
            MaxDiscountAmount = maxDiscountAmount,
            CurrentUses = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        
        coupon.AddDomainEvent(new CouponCreatedEvent(coupon.Id, coupon.Code));
        
        return coupon;
    }
    
    public void Activate()
    {
        if (Status != CouponStatus.Draft)
            throw new InvalidOperationException("Only draft coupons can be activated");
            
        Status = CouponStatus.Active;
        IsActive = true;
        UpdatedAt = DateTime.UtcNow;
        
        AddDomainEvent(new CouponActivatedEvent(Id));
    }
    
    public void IncrementUsageCount()
    {
        if (Limits.MaxTotalUses.HasValue && CurrentUses >= Limits.MaxTotalUses.Value)
            throw new InvalidOperationException("Coupon usage limit exceeded");
            
        CurrentUses++;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void DecrementUsageCount()
    {
        if (CurrentUses > 0)
        {
            CurrentUses--;
            UpdatedAt = DateTime.UtcNow;
        }
    }
    
    public void Delete()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }
}
