using chainofreponsability.Models;

namespace chainofreponsability.Services;

abstract class RequestHandler
{
    protected RequestHandler? _nextHandler;

    public void Next(RequestHandler nextHandler)
    {
        _nextHandler = nextHandler;
    }

    public abstract void Handle(RequestContext ctx);
}
