using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;

namespace WebApplication1.Controllers;

[Authorize(Roles = ("Admin"))]
public class UsersController : Controller
{
    readonly MVCD06DbContext _dbContext;
    public UsersController(MVCD06DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var users = _dbContext.Users.Include(u => u.Roles).ToList();

        return View(users);
    }
}
