using Microsoft.AspNetCore.Mvc;
using MVCD01.Models;

namespace MVCD01.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string Display()
        {
            return "Welcom MVC!";
        }

        public IActionResult Show()
        {
            return View();
        }

        public IActionResult ShowAdd()
        {
            return View();
        }

        public int Add(int x, int y)
        {
            return x + y;
        }

        public IActionResult CreateStd()
        {
            return View();
        }

        public string AddStd(Student std)
        {
            return new Student() { Id = new Random().Next(1, 10), Name = std.Name, Age = std.Age }.ToString();
        }
    }
}
