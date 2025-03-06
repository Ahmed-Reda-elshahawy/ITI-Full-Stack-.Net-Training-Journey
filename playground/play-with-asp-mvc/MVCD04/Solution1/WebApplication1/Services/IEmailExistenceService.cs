namespace WebApplication1.Services;

public interface IEmailExistenceService
{
    bool CheckEmailExistence(string email, int? id);
}
