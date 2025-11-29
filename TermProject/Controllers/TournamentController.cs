using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                    Id = t.TeamId,
                    TeamName = t.TeamName,
                    Division = t.Division.DivisionName,
                    Players = _db.Player.Where(p => p.TeamId == t.TeamId)
                    .Select(p => new PlayerVm
                    {
                        Name = p.PlayerName,
                        City = p.City,
                        Province = p.Province
                    }).ToList()
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

        public IActionResult TeamDetails(int id)
        {
            //get the team that matchs the id passed in
            //including the division here so that it is not null
            var team = _db.Team
                .Include(t=>t.Division)
                .FirstOrDefault(t => t.TeamId == id);

            //show error if team is not found/does not exist
            if (team == null)
            {
                return NotFound();
            }

            //LINQ query used to get the detail information
            //Using the TeamVm to pass it to the view
            var vm = new TeamVm
            {
                //getting tthe team Id, Name, and Division Name
                Id = team.TeamId,
                TeamName = team.TeamName,
                Division = team.Division.DivisionName,
                //getting all the players who have that team Id identifier
                Players = _db.Player
                .Where(p=>p.TeamId == team.TeamId)
                //using the PlayerVm to get the player info
                .Select(p => new PlayerVm
                {
                    TeamId = team.TeamId,
                    Name = p.PlayerName,
                    City = p.City,
                    Province = p.Province
                })
                .ToList()
            };

            return View(vm);
        }
    }
}
