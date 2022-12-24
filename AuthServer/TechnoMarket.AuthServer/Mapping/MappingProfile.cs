using AutoMapper;
using TechnoMarket.AuthServer.Dtos;
using TechnoMarket.AuthServer.Models;

namespace TechnoMarket.Services.Order.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserAppDto, UserApp>().ReverseMap();
        }
    }
}
