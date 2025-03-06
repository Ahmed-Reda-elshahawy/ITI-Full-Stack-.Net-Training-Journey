using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Departments
{
    public class UpdateModel(IRepository<Department> _repo) : PageModel
    {
        [BindProperty]
        public Department Department { get; set; }
        public void OnGet([FromRoute] int id)
        {
            Department = _repo.GetById(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _repo.Update(Department);
                _repo.Save();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
