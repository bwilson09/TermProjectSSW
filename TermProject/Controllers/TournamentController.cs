using Microsoft.AspNetCore.Mvc;
using TermProject.Models;
using TermProject.ViewModels;

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
            //LINQ query used to get the teams that have paid their registration
            var teams = _db.Team
                .Where(t => t.RegistrationPaid == true)
                //creating a new vm that shows just the team name, division and the players names
                .Select(t => new TeamVm
                {
                    TeamName = t.TeamName,
                    Division = t.Division,
                    Players = _db.Player.Where(p => p.TeamId == t.TeamId).Select(p => p.PlayerName).ToList()
                })
                //also checking here to make sure that there are 4 players in the paid regisration teams
                .Where(vm => vm.Players.Count == 4)
                //setting it to list in order to display on the page
                .ToList();
            return View(teams);
        }

        public IActionResult Register()
        {
            return View();
        }
    }
}
