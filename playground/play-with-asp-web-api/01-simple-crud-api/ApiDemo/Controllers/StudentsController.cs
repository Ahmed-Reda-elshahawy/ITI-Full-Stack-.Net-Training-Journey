using ApiDemo.Dtos.Students;
using ApiDemo.Dtos.Shared;
using ApiDemo.Models;
using ApiDemo.UnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApiDemo.Controllers;

[Consumes("application/json")]
[Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class StudentsController(IUnitOfWork _unitOfWork, IMapper _mapper) : ControllerBase
{
    [EndpointSummary("Fetch all Students.")]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var repo = _unitOfWork.Repository<Student>();
        var paginationDto = new PaginationDto(page, pageSize, await repo.CountAsync());
        var students = await _unitOfWork.Repository<Student>().GetAllAsync(paginationDto);
        var responseData = _mapper.Map<List<GetStudentResponseDto>>(students);

        return Ok(new ResponseDto(responseData, paginationDto));
    }

    [EndpointSummary("Fetch Student By Id.")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var repo = _unitOfWork.Repository<Student>();
        var existingStd = await repo.GetByIdAsync(id);

        if (existingStd == null)
        {
            return NotFound(new ResponseDto([$"Student with id [{id}] is not found!"]));
        }
        
        var responseData = _mapper.Map<GetStudentResponseDto>(existingStd);

        return Ok(new ResponseDto(responseData));
    }

    [EndpointSummary("Create new Student.")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStudentRequestDto dto)
    {
        var deptRepo = _unitOfWork.Repository<Department>();
        var existingDepartment = await deptRepo.GetByIdAsync(dto.DeptId);

        if (existingDepartment == null)
        {
            return BadRequest(new ResponseDto([$"Department with id [{dto.DeptId}] is not found!"]));
        }

        var stdRepo = _unitOfWork.Repository<Student>();
        var std = _mapper.Map<Student>(dto);
        var createdStd = await stdRepo.CreateAsync(std);
        await _unitOfWork.SaveChangesAsync();
        var responseData = _mapper.Map<GetStudentResponseDto>(createdStd);

        return CreatedAtAction(
            nameof(GetById),
            new { id = createdStd.Id },
            new ResponseDto(responseData)
        );
    }

    [EndpointSummary("Update an existing Student By Id.")]
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateById([FromRoute]int id, [FromBody] UpdateStudentRequestDto dto)
    {
        var stdRepo = _unitOfWork.Repository<Student>();
        var existingStd = await stdRepo.GetByIdAsync(id);
        
        if (existingStd == null)
        {
            return BadRequest(new ResponseDto([$"Student with id [{id}] not found!"]));
        }

        _mapper.Map(dto, existingStd);

        var updatedStd = await stdRepo.UpdateASync(existingStd);
        await _unitOfWork.SaveChangesAsync();

        var responseData = _mapper.Map<GetStudentResponseDto>(updatedStd);

        return Ok(new ResponseDto(responseData));
    }

    [EndpointSummary("Delete an existing Student By Id.")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteById([FromRoute] int id)
    {
        var stdRepo = _unitOfWork.Repository<Student>();
        var existingStd = await stdRepo.GetByIdAsync(id);

        if (existingStd == null)
        {
            return BadRequest(new ResponseDto([$"Student with id [{id}] not found!"]));
        }

        await stdRepo.DeleteAsync(existingStd);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
}
