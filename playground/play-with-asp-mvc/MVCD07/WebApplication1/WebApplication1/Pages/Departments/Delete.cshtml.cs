using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Departments
{
    public class DeleteModel(IRepository<Department> _repo) : PageModel
    {
        [BindProperty]
        public Department Department { get; set; }

        public IActionResult OnGet(int id)
        {
            Department = _repo.GetById(id);

            if (Department == null)
            {
                return RedirectToPage("Index");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            _repo.Delete(Department);
            _repo.Save();
            return RedirectToAction("Index");
        }
    }
}
