using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        private MVCD03DbContext _db = new MVCD03DbContext();
        public IActionResult Index()
        {
            return View(_db.Departments.ToList());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if (!ModelState.IsValid)
            {
                return View(dept);
            }
            _db.Departments.Add(dept);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _db.Departments.FirstOrDefault(d => d.Id == id);

            if (dept == null)
            {
                return NotFound();
            }

            return View(dept);
        }

        [HttpPost]
        public IActionResult Edit(Department dept)
        {
            if (!ModelState.IsValid)
                return View(dept);
            _db.Departments.Update(dept);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var dept = _db.Departments.Include(d => d.Students).FirstOrDefault(d => d.Id == id);

            if (dept == null)
            {
                return NotFound();
            }

            if (dept.Students.Count() <= 0)
            {
                _db.Departments.Remove(dept);
                _db.SaveChanges();
                TempData["SuccessMessage"] = "Department deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Can't Delete Department, because there's students enrolled!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckNameExistance(string name, int? id)
        {
            bool nameExists = id == null
                ? _db.Departments.Any(d => d.Name == name)
                : _db.Departments.Any(d => d.Name == name && d.Id != id);

            if (nameExists)
            {
                return Json($"The name '{name}' is already exist.");
            }

            return Json(true);
        }
    }
}
