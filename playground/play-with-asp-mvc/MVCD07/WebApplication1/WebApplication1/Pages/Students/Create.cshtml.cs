using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repositories;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Students
{
    public class CreateModel(IRepository<Student> _studentRepo, IRepository<Department> _departmentRepo) : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }


        public List<Department> Departments { get; set; }

        public void OnGet()
        {
            Departments = _departmentRepo.GetAll();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _studentRepo.Create(Student);
                _studentRepo.Save();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
