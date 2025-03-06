using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Database;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View(_departmentService.GetAll());
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
            _departmentService.Create(dept);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var dept = _departmentService.GetById(id);

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
            _departmentService.Update(dept);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var dept = _departmentService.GetById(id);

            if (dept == null)
            {
                return NotFound();
            }

            if (dept.Students.Count() <= 0)
            {
                _departmentService.Delete(id);
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
            bool nameExists = _departmentService.CheckNameExistance(name, id);

            if (nameExists)
            {
                return Json($"The name '{name}' is already exist.");
            }

            return Json(true);
        }
    }
}
