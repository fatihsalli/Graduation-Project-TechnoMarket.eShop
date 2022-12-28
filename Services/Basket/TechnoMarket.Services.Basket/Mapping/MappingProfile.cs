using AutoMapper;
using TechnoMarket.Services.Basket.Dtos;
using TechnoMarket.Shared.Messages;

namespace TechnoMarket.Services.Basket.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BasketCheckOutDto, CreateOrderMessageCommand>();
        }
    }
}
