using AutoMapper;
using TechnoMarket.Services.Catalog.Dtos;
using TechnoMarket.Services.Catalog.Models;

namespace TechnoMarket.Services.Catalog.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductWithCategoryDto>()
                .ForMember(p => p.ProductFeature,
                    opt => opt.MapFrom(x => string.Join(' ', $"Color:{x.Feature.Color}", $"Width:{x.Feature.Width}", $"Height:{x.Feature.Height}", $"Weight:{x.Feature.Weight}"))).ReverseMap();

            CreateMap<Product, ProductDto>()
                .ForMember(p => p.ProductFeature,
                    opt => opt.MapFrom(x => string.Join(' ', $"Color:{x.Feature.Color}", $"Width:{x.Feature.Width}", $"Height:{x.Feature.Height}", $"Weight:{x.Feature.Weight}"))).ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
