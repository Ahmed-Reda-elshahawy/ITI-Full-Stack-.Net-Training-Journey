using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetFile()
        {
            return File(@"~/assets/file1.txt", "text/plain", "file1.txt");
        }

        public IActionResult GetJson()
        {
            return Json(new { status = "success", message = "Json returned succesfully" });
        }
    }
}
