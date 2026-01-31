namespace CouponSystem.Domain.Aggregates.Redemption;

public record RedemptionId(Guid Value)
{
    public static RedemptionId New() => new(Guid.NewGuid());
    public static RedemptionId From(Guid value) => new(value);
}
