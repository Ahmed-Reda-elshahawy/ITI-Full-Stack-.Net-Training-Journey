using observer.Models;

namespace observer.Services.Users;

class UserCreatedEventArgs:EventArgs
{

    public UserCreatedEventArgs(User user)
    {
        User = user;
    }

    public User User { get; }
}
