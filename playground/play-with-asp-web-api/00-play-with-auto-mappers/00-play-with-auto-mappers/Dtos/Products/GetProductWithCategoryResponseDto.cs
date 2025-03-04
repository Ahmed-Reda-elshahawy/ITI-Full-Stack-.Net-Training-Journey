using _00_play_with_auto_mappers.Models;

namespace _00_play_with_auto_mappers.Dtos.Products;

public class GetProductWithCategoryResponseDto:GetProductResponseDto
{
    public Category Category{ get; set; }
}
