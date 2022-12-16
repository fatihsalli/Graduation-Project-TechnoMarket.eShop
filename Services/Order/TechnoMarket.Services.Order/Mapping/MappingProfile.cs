using AutoMapper;
using TechnoMarket.Services.Order.Dtos;
using TechnoMarket.Services.Order.Models;

namespace TechnoMarket.Services.Order.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Order, OrderDto>()
                .ForMember(order => order.FullAddress,
                opt => opt.MapFrom(x => string.Join(' ', x.Address.AddressLine, x.Address.Country, x.Address.City, x.Address.CityCode))).ReverseMap();

            CreateMap<Models.Order, OrderCreateDto>().ReverseMap();
            CreateMap<Models.Order, OrderUpdateDto>().ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
