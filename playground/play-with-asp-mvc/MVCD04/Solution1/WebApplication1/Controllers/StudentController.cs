using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Database;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        IStudentService _studentService;
        IEmailExistenceService _emailExistanceService;
        IDepartmentService _departmentService;

        public StudentController(IStudentService studentService, IEmailExistenceService emailExistanceService, IDepartmentService departmentService)
        {
            _studentService = studentService;
            _emailExistanceService = emailExistanceService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var stds = _studentService.GetAll();

            return View(stds);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Departments = _departmentService.GetAll();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student std)
        {
            if (!ModelState.IsValid)
            {
                return View(std);
            }

            _studentService.Create(std);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var std = _studentService.GetById(id);

            if (std == null)
            {
                return NotFound();
            }

            ViewBag.Departments = _departmentService.GetAll();

            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            if (!ModelState.IsValid)
                return View(std);

            _studentService.Update(std);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var std = _studentService.Delete(id);

            if (std == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CheckEmailExistence(string email, int? id)
        {
            bool emailExists = _emailExistanceService.CheckEmailExistence(email, id);

            if (emailExists)
            {
                return Json($"The email '{email}' is already registered.");
            }

            return Json(true);
        }

    }
}
