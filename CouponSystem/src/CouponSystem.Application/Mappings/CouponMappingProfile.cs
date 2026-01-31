using AutoMapper;
using CouponSystem.Application.DTOs;
using CouponSystem.Domain.Aggregates.Coupon;

namespace CouponSystem.Application.Mappings;

public class CouponMappingProfile : Profile
{
    public CouponMappingProfile()
    {
        CreateMap<Coupon, CouponDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code.Value))
            .ForMember(dest => dest.DiscountType, opt => opt.MapFrom(src => src.Discount.Type.ToString()))
            .ForMember(dest => dest.DiscountValue, opt => opt.MapFrom(src => src.Discount.Value))
            .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.ValidityPeriod.StartDate))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.ValidityPeriod.EndDate))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
