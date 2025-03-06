using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Students
{
    public class UpdateModel(IRepository<Student> _studentRepo, IRepository<Department> _departmentRepo) : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }
        public List<Department> Departments { get; set; }

        public void OnGet(int id)
        {
            Departments = _departmentRepo.GetAll();
            Student = _studentRepo.GetById(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _studentRepo.Update(Student);
                _studentRepo.Save();

                return RedirectToPage("Index");
            }

            return Page();

        }
    }
}
