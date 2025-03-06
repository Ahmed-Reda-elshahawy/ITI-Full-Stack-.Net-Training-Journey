using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Departments
{
    public class IndexModel(IRepository<Department> _repo) : PageModel
    {
        public List<Department> Departments { get; set; }
        public void OnGet()
        {
            Departments = _repo.GetAll();
        }
    }
}
