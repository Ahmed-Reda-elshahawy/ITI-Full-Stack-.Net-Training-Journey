using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Models;
using WebApplication1.Repository;

namespace WebApplication1.Pages.Students
{
    public class IndexModel(IRepository<Student> _repo) : PageModel
    {
        public List<Student> Students { get; set; }
        public void OnGet()
        {
            Students = _repo.GetAll();
        }
    }
}
