using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        private MVCD03DbContext _db = new MVCD03DbContext();

        [HttpGet]
        public IActionResult Index()
        {
            var stds = _db.Students.Include(s => s.Department).ToList();

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
            if (!ModelState.IsValid)
            {
                return View(std);
            }
            _db.Students.Add(std);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var std = _db.Students.FirstOrDefault(s => s.Id == id);

            if (std == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _db.Departments.ToList();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (!ModelState.IsValid)
                return View(std);
            _db.Students.Update(std);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var std = _db.Students.FirstOrDefault(s => s.Id == id);

            if (std == null)
            {
                return NotFound();
            }

            _db.Students.Remove(std);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckEmailExistence(string email, int? id)
        {
            bool emailExists = id == null
                ? _db.Students.Any(s => s.Email == email)
                : _db.Students.Any(s => s.Email == email && s.Id != id);

            if (emailExists)
            {
                return Json($"The email '{email}' is already registered.");
            }

            return Json(true);
        }

    }
}
