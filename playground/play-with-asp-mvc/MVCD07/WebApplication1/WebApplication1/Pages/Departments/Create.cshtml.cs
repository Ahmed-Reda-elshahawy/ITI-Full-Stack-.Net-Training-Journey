using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Departments
{
    public class CreateModel(IRepository<Department> _repo) : PageModel
    {
        [BindProperty]
        public Department Department { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _repo.Create(Department);
                _repo.Save();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
