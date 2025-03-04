using _00_play_with_auto_mappers.Dtos.Products;
using _00_play_with_auto_mappers.Models;
using AutoMapper;

namespace _00_play_with_auto_mappers.AutoMappersProfiles;

public class ProductsAutoMapperProfile:Profile
{
    public ProductsAutoMapperProfile()
    {
        CreateMap<Product, GetProductResponseDto>().AfterMap((src, dest) =>
        {
            dest.CategoryName = src?.Category?.Name;
        });

        CreateMap<Product, GetProductWithCategoryResponseDto>().AfterMap((src, dest) =>
        {
            dest.CategoryName = src?.Category?.Name;
        });
    }
}
