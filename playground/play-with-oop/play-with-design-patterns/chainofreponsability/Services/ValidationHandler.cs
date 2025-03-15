using chainofreponsability.Models;

namespace chainofreponsability.Services;

class ValidationHandler : RequestHandler
{
    public override void Handle(RequestContext ctx)
    {
        if (string.IsNullOrEmpty(ctx.Data))
        {
            Console.WriteLine("Validation Failed: No data provided!");
            return;
        }
        Console.WriteLine("Validation Passed.");
        _nextHandler?.Handle(ctx);
    }
}
