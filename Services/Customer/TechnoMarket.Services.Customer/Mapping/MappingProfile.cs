using AutoMapper;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;

namespace TechnoMarket.Services.Customer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Customer, CustomerCreateDto>().ReverseMap();

            CreateMap<Models.Customer, CustomerUpdateDto>()
                .ForMember(customerDto => customerDto.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ReverseMap();

            CreateMap<Models.Customer, CustomerDto>()
                .ForMember(customerDto => customerDto.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ReverseMap();

            CreateMap<Address, AddressDto>().ReverseMap();
        }

    }
}
