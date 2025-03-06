using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Students
{
    public class DeleteModel(IRepository<Student> _studentRepo) : PageModel
    {
        [BindProperty]
        public Student Student { get; set; }

        public void OnGet(int id)
        {
            Student = _studentRepo.GetById(id);
        }

        public IActionResult OnPost()
        {
            _studentRepo.Delete(Student);
            _studentRepo.Save();

            return RedirectToPage("Index");
        }
    }
}
