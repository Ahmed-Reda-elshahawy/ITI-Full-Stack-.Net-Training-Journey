using ApiDemo.Dtos.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiDemo.Filters;

public class ModelStateValidationFilter(IMapper _mapper) : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)  {}

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var responseDto = new ResponseDto(_mapper.Map<List<string>>(context.ModelState));
            context.Result = new BadRequestObjectResult(responseDto);
        }
    }
}
