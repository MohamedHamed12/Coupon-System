using System.Text.RegularExpressions;
using CouponSystem.Domain.Common;

namespace CouponSystem.Domain.Aggregates.Coupon;

public record CouponCode : ValueObject
{
    private const int MinLength = 4;
    private const int MaxLength = 50;
    private static readonly Regex ValidCodePattern = new(@"^[A-Z0-9-]+$", RegexOptions.Compiled);
    
    public string Value { get; init; }
    
    public CouponCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Coupon code cannot be empty");
            
        var normalized = value.Trim().ToUpperInvariant();
        
        if (normalized.Length < MinLength || normalized.Length > MaxLength)
            throw new ArgumentException($"Coupon code must be between {MinLength} and {MaxLength} characters");
            
        if (!ValidCodePattern.IsMatch(normalized))
            throw new ArgumentException("Coupon code can only contain letters, numbers, and hyphens");
            
        Value = normalized;
    }
    
    public static implicit operator string(CouponCode code) => code.Value;
    
    public static CouponCode Generate(int length = 8)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        var code = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
        return new CouponCode(code);
    }
}
