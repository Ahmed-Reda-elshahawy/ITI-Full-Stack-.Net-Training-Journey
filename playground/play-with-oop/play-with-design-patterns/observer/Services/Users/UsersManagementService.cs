using observer.Models;

namespace observer.Services.Users;

class UsersManagementService
{
    private readonly List<User> _users;
    public event EventHandler<UserCreatedEventArgs> UserCreated;

    public UsersManagementService()
    {
        _users = [
            new User {Name = "John Doe"},
            new User {Name = "Jim Carter"}
            ];
    }

    protected virtual void OnUserCreated(UserCreatedEventArgs e)
    {
        UserCreated?.Invoke(this, e);
    }

    public List<User> GetAllUsers()
    {
        return _users;
    }

    public User? GetUserById(int id)
    {
        return _users.FirstOrDefault(u => u.Id == id);
    }

    public void CreateUser(User user)
    {
        _users.Add(user);
        OnUserCreated(new UserCreatedEventArgs(user));
    }
}
