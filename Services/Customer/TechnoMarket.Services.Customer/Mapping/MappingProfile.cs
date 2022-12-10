﻿using AutoMapper;
using TechnoMarket.Services.Customer.Dtos;
using TechnoMarket.Services.Customer.Models;

namespace TechnoMarket.Services.Customer.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Models.Customer, CustomerDtoWithAddress>().ReverseMap();
            CreateMap<Models.Customer, CustomerCreateDto>().ReverseMap();
            CreateMap<Models.Customer, CustomerUpdateDto>().ReverseMap();
            CreateMap<Models.Customer, CustomerDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }

    }
}
