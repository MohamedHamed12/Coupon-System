using MediatR;
using AutoMapper;
using CouponSystem.Application.DTOs;
using CouponSystem.Domain.Aggregates.Coupon;
using CouponSystem.Domain.Repositories;
using CouponSystem.Domain.Enums;

namespace CouponSystem.Application.Commands;

public class CreateCouponCommandHandler : IRequestHandler<CreateCouponCommand, CouponDto>
{
    private readonly ICouponRepository _repository;
    private readonly IMapper _mapper;
    
    public CreateCouponCommandHandler(ICouponRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<CouponDto> Handle(CreateCouponCommand request, CancellationToken cancellationToken)
    {
        var code = string.IsNullOrEmpty(request.Code) 
            ? CouponCode.Generate() 
            : new CouponCode(request.Code);
        
        var discountType = Enum.Parse<DiscountType>(request.DiscountType, true);
        var discount = new DiscountConfiguration(discountType, request.DiscountValue);
        
        var limits = new UsageLimits
        {
            MaxTotalUses = request.MaxTotalUses,
            MaxUsesPerUser = request.MaxUsesPerUser
        };
        
        var dateRange = new DateRange(request.StartDate, request.EndDate);
        
        var coupon = Coupon.Create(code, discount, limits, dateRange, request.MaxDiscountAmount);
        
        await _repository.AddAsync(coupon, cancellationToken);
        
        return _mapper.Map<CouponDto>(coupon);
    }
}
