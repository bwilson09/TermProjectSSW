using Microsoft.AspNetCore.Mvc;
using TermProject.Models;

namespace TermProject.Controllers
{
    public class TournamentController : Controller
    {

        private readonly TournamentDbContext _db;

        //constructor receives the inject DbContext
        public TournamentController(TournamentDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
