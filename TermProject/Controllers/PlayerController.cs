using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class PlayerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
