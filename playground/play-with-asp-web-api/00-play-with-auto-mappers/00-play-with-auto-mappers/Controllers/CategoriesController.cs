using _00_play_with_auto_mappers.Database;
using _00_play_with_auto_mappers.Dtos.Categories;
using _00_play_with_auto_mappers.Dtos.Pagination;
using _00_play_with_auto_mappers.Dtos.ResponseBase;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _00_play_with_auto_mappers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(AppDbContext _dbContext, IMapper _mapper) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var paginationDto = new PaginationDto(page, pageSize, await _dbContext.Categories.CountAsync());
        var categories = await _dbContext
                .Categories
                .Skip(paginationDto.Skip)
                .Take(paginationDto.Take)
                .ToListAsync();

        var dataDto = _mapper.Map<List<GetCategoryResponseDto>>(categories);

        return Ok(new ResponseBaseDto(dataDto, paginationDto));
    }

    [Route("{id:int}")]
    [HttpGet]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var category = await _dbContext.Categories.FindAsync(id);

        if (category == null)
        {
            return NotFound(new ResponseBaseDto([$"Category with id [{id}] is not found!"]));
        }

        var dataDto = _mapper.Map<GetCategoryResponseDto>(category);

        return Ok(dataDto);
    }
}
