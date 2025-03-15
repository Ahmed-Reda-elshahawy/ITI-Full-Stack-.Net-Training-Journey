using chainofreponsability.Models;

namespace chainofreponsability.Services;

class LogHandler : RequestHandler
{
    public override void Handle(RequestContext ctx)
    {
        Console.WriteLine($"Loggin Request[{ctx.Id}]: Data = {ctx.Data}");
        _nextHandler?.Handle(ctx);
    }
}
