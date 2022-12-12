using AutoMapper;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>()
                .ForMember(categoryDto => categoryDto.Id, opt => opt.MapFrom(x => x.Id.ToString())).ReverseMap();
            CreateMap<Category, CategoryCreateDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductDto>()
                .ForMember(productDto => productDto.Id, opt => opt.MapFrom(x => x.Id.ToString()))
                .ForMember(p => p.ProductFeature,
                    opt => opt.MapFrom(x => string.Join(' ', x.Feature.Color, x.Feature.Width, x.Feature.Height, x.Feature.Weight))).ReverseMap();
        }
    }
}
