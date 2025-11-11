using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class TeamController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
