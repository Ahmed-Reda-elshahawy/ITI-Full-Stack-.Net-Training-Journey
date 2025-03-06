using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class AccountsController : Controller
{
    MVCD06DbContext _dbContext;
    public AccountsController(MVCD06DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public IActionResult Index()
    //{
    //    return View();
    //}

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
    {
        if (ModelState.IsValid)
        {
            var user = await _dbContext.Users.Include(u => u.Roles).SingleOrDefaultAsync(u => u.Email == model.Email);

            if (user.Password == model.Password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };

                foreach (var role in user.Roles)
                {
                    claims.Add(new(ClaimTypes.Role, role.Name));
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrincipal);

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return RedirectToAction("index", "home");
            }

            ModelState.AddModelError("", "Invalid Email or Password!");
        }
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        if (User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        return RedirectToAction("Index", "Home");
    }

    public IActionResult AccessDenied()
    {
        HttpContext.Response.StatusCode = 401;
        return View();
    }
}
