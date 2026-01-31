using CouponSystem.Domain.Common;

namespace CouponSystem.Domain.Aggregates.Coupon;

public record UsageLimits : ValueObject
{
    public int? MaxTotalUses { get; init; }
    public int? MaxUsesPerUser { get; init; }
    public int? MaxUsesPerDay { get; init; }
    
    public bool IsExceeded(int currentTotalUses, int? currentDailyUses = null)
    {
        if (MaxTotalUses.HasValue && currentTotalUses >= MaxTotalUses.Value)
            return true;
            
        if (MaxUsesPerDay.HasValue && currentDailyUses.HasValue && 
            currentDailyUses >= MaxUsesPerDay.Value)
            return true;
            
        return false;
    }
    
    public bool CanBeUsedByUser(int userUsageCount)
    {
        if (!MaxUsesPerUser.HasValue)
            return true;
            
        return userUsageCount < MaxUsesPerUser.Value;
    }
    
    public static UsageLimits Unlimited() => new();
    
    public static UsageLimits SingleUse() => new() 
    { 
        MaxTotalUses = 1, 
        MaxUsesPerUser = 1 
    };
}
