using Microsoft.AspNetCore.Mvc;

namespace TermProject.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
