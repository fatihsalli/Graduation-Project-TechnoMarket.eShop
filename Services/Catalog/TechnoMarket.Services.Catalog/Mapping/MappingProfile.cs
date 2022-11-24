using AutoMapper;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Category,CategoryDto>().ReverseMap();

            CreateMap<ProductFeature, FeatureDto>().ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductFeature,
                    opt => opt.MapFrom(x => string.Join(' ', x.Feature.Color, x.Feature.Summary)));

            CreateMap<Product, ProductCreateDto>().ReverseMap();
            CreateMap<Product, ProductUpdateDto>().ReverseMap();
        }




    }
}
