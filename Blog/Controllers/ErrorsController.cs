using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class ErrorsController : Controller
    {
        
        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult SomethingWentWrong()
        {
            return View();
        }
    }
}
