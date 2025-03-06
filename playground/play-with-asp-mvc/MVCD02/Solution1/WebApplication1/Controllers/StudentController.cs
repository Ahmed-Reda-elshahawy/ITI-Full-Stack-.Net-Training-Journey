using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication2.Database;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        MVCD02DbContext _db = new MVCD02DbContext();
        public IActionResult Index()
        {
            var stds = _db.Students.ToList();
            return View(stds);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _db.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            _db.Students.Add(std);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
