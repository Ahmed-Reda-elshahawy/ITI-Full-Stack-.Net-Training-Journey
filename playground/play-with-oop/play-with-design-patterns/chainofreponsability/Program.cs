using chainofreponsability.Models;
using chainofreponsability.Services;

RequestContext requestCtx = new() { Data = "Hello world!", UserName = "John Doe!" };
AuthHandler authHandler = new AuthHandler();
LogHandler logHandler = new LogHandler();
ValidationHandler validationHandler = new ValidationHandler();

validationHandler.Next(authHandler);
authHandler.Next(logHandler);

validationHandler.Handle(requestCtx);