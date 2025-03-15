using chainofreponsability.Models;

namespace chainofreponsability.Services;

class AuthHandler : RequestHandler
{
    public override void Handle(RequestContext ctx)
    {
        if (string.IsNullOrEmpty(ctx?.UserName))
        {
            Console.WriteLine("Authentication Failed: No UserName Provided!");
            return;
        }

        Console.WriteLine("Authentication Passed!");
        ctx.IsAuthenticated = true;
        _nextHandler?.Handle(ctx);
    }
}
