using CouponSystem.Domain.Common;

namespace CouponSystem.Domain.Aggregates.Coupon;

public record DateRange : ValueObject
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
    
    public DateRange(DateTime startDate, DateTime endDate)
    {
        if (endDate <= startDate)
            throw new ArgumentException("End date must be after start date");
            
        StartDate = startDate;
        EndDate = endDate;
    }
    
    public bool IsActiveAt(DateTime checkTime)
    {
        return checkTime >= StartDate && checkTime <= EndDate;
    }
    
    public TimeSpan Duration => EndDate - StartDate;
}
