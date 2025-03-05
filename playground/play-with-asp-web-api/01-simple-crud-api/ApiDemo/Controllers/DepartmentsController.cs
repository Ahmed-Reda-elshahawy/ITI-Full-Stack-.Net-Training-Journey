using ApiDemo.Dtos.Departments;
using ApiDemo.Dtos.Shared;
using ApiDemo.Models;
using ApiDemo.UnitOfWorks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers;

[Consumes("application/json")]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class DepartmentsController(IUnitOfWork _unitOfWork, IMapper _mapper) : ControllerBase
{
    [EndpointSummary("Fetch all Departments.")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var deptRepo = _unitOfWork.Repository<Department>();

        var paginationDto = new PaginationDto(page, pageSize, await deptRepo.CountAsync());

        var departments = await deptRepo.GetAllAsync(paginationDto);

        var responseData = _mapper.Map<List<GetDepartmentResponseDto>>(departments);

        return Ok(new ResponseDto(responseData, paginationDto));
    }

    [EndpointSummary("Fetch Department By Id.")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var repo = _unitOfWork.Repository<Department>();
        var existingDept = await repo.GetByIdAsync(id);

        if (existingDept == null)
        {
            return NotFound(new ResponseDto([$"Department with id [{id}] is not found!"]));
        }

        var responseData = _mapper.Map<GetDepartmentResponseDto>(existingDept);

        return Ok(new ResponseDto(responseData));
    }

    [EndpointSummary("Create new Department.")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentRequestDto dto)
    {
        var deptRepo = _unitOfWork.Repository<Department>();
        var dept = _mapper.Map<Department>(dto);
        var createdDept = await deptRepo.CreateAsync(dept);
        await _unitOfWork.SaveChangesAsync();
        var responseData = _mapper.Map<GetDepartmentResponseDto>(createdDept);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdDept.Id },
            new ResponseDto(responseData)
        );
    }

    [EndpointSummary("Update an existing Department By Id.")]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] UpdateDepartmentRequestDto dto)
    {
        var deptRepo = _unitOfWork.Repository<Department>();

        var existingDept = await deptRepo.GetByIdAsync(id);

        if (existingDept == null)
        {
            return BadRequest(new ResponseDto([$"Department with id [{id}] not found!"]));
        }

        _mapper.Map(dto, existingDept);

        var updatedDept = await deptRepo.UpdateASync(existingDept);
        await deptRepo.SaveChangesAsync();

        var responseData = _mapper.Map<GetDepartmentResponseDto>(updatedDept);

        return Ok(new ResponseDto(responseData));
    }

    [EndpointSummary("Delete an existing Department By Id.")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById([FromRoute] int id)
    {
        var deptRepo = _unitOfWork.Repository<Department>();
        var existingDept = await deptRepo.GetByIdAsync(id);

        if (existingDept == null)
        {
            return BadRequest(new ResponseDto([$"Department with id [{id}] not found!"]));
        }

        await deptRepo.DeleteAsync(existingDept);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}
