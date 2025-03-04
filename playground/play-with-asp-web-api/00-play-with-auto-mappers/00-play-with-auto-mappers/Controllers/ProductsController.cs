using _00_play_with_auto_mappers.Database;
using _00_play_with_auto_mappers.Dtos.Pagination;
using _00_play_with_auto_mappers.Dtos.Products;
using _00_play_with_auto_mappers.Dtos.ResponseBase;
using _00_play_with_auto_mappers.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _00_play_with_auto_mappers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(AppDbContext _dbContext, IMapper _mapper) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var paginationDto = new PaginationDto(page, pageSize, await _dbContext.Products.CountAsync());
        var products = await _dbContext
                .Products
                .Include(p => p.Category)
                .Skip(paginationDto.Skip)
                .Take(paginationDto.Take)
                .ToListAsync();
        var dataDto = _mapper.Map<List<GetProductResponseDto>>(products);
        return Ok(new ResponseBaseDto(dataDto, paginationDto));
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] int id, [FromQuery] bool withCategory = false)
    {
        var product = await _dbContext.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        if (product == null)
        {
            return NotFound(new ResponseBaseDto([$"Product with id [{id}] is not found!"]));
        }

        object dataDto;
        
        if (withCategory)
        {
            dataDto= _mapper.Map<GetProductWithCategoryResponseDto>(product);
        }
        else
        {
            dataDto = _mapper.Map<GetProductResponseDto>(product);
        }
        return Ok(new ResponseBaseDto(dataDto));
    }
}
