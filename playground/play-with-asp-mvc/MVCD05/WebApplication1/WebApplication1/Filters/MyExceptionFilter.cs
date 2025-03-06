using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
namespace WebApplication1.Filters;

public class MyExceptionFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            context.HttpContext.Response.StatusCode = 500;
            context.ExceptionHandled = true;
            context.Result = new ViewResult
            {
                ViewName = "~/Views/Error.cshtml"
            };
        }
        base.OnActionExecuted(context);
    }
}
