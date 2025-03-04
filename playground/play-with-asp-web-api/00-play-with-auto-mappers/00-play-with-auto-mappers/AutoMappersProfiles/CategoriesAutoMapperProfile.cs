using _00_play_with_auto_mappers.Dtos.Categories;
using _00_play_with_auto_mappers.Models;
using AutoMapper;

namespace _00_play_with_auto_mappers.AutoMappersProfiles;

public class CategoriesAutoMapperProfile:Profile
{
    public CategoriesAutoMapperProfile()
    {
        CreateMap<Category, GetCategoryResponseDto>().AfterMap((src, dest) =>
        {
            dest.ProductsCount = src.Products.Count;
        });
    }
}
