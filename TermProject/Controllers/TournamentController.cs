using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
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



        public IActionResult TeamDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //get the team that matchs the id passed in
            //including the division here so that it is not null
            var team = _db.Team
                .Include(t => t.Division)
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
                .Where(p => p.TeamId == team.TeamId)
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

        [HttpGet]
        public IActionResult Register()
        {
            //getting the team vm
            var vm = new TeamRegisterVm
            {
                //getting a list of the playvm
                Players = new List<PlayerRegisterVm>
                {
                    //adding 4 playerVm here as 4 players are needed for a team
                    //allows sections for all 4 of the players to be added
                    new PlayerRegisterVm(),
                    new PlayerRegisterVm(),
                    new PlayerRegisterVm(),
                    new PlayerRegisterVm()
                },
                //populating the list in the vm
                //accessing the db names from division table
                Divisions = _db.Division
                .Select(d => new SelectListItem
                {
                    Value = d.DivisionId.ToString(),
                    Text = d.DivisionName
                })
                .ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Register(TeamRegisterVm vm)
        {
            if (vm == null)
            {
                return NotFound();
            }
            //checking to make sure teamname doesnt already exist
            if (_db.Team.Any(t=>t.TeamName == vm.TeamName))
            {
                ModelState.AddModelError("TeamName", "This team name already exists, please try another one");
            }

            //checking to make sure 4 player info was provided
            if(vm.Players == null || vm.Players.Count != 4)
            {
                ModelState.AddModelError("Players", "You must register exactly 4 players for your team");
            }

            //check if any of the validation has failed and redisplay the form
            if (!ModelState.IsValid) {
                //need to repopulate the divisions for the dropdown
                vm.Divisions = _db.Division
                .Select(d => new SelectListItem
                {
                    Value = d.DivisionId.ToString(),
                    Text = d.DivisionName
                })
                .ToList();

                return View(vm);
            }

            //if no validation errors and nothing is left blank- add the team to db
            var team = new Team
            {
                TeamName = vm.TeamName,
                DivisionId = (int)vm.DivisionId.Value,
                RegistrationPaid = false,
                PaymentDate = null
            };
            //add the team and save the changes to generate the team id
            _db.Team.Add(team);
            _db.SaveChanges();

           //add all the players by looping through the list of players
           foreach (var p in vm.Players)
            {
                var player = new Player
                {
                    PlayerName = p.PlayerName,
                    City = p.City,
                    Province = p.Province,
                    Email = p.Email,
                    Phone = p.Phone,
                    //add team id to connect players to their team
                    TeamId = team.TeamId
                };
                //add the players to the db
                _db.Player.Add(player);
            }
           _db.SaveChanges();

            //send a success message back to the register page for users
            TempData["Success"] = "Thank you for registering! Your form has been received and you will receive an email from us once your registration fee has processed.";

            return RedirectToAction("Index", "Tournament");
        }

    }
}
