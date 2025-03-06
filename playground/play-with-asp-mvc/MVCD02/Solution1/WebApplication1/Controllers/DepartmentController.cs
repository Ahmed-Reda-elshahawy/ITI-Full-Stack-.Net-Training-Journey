using Microsoft.AspNetCore.Mvc;
using WebApplication2.Database;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        MVCD02DbContext _db = new MVCD02DbContext();

        public IActionResult Index()
        {
            var depts = _db.Departments.ToList();
            return View(depts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            _db.Departments.Add(dept);
            _db.SaveChanges();
            return RedirectToAction("index");
        }
    }
}
