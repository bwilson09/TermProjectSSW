using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
