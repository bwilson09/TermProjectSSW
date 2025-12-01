using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TermProject.Models;
using TermProject.ViewModels;

namespace TermProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private bool IsAdmin()
        {
            return User.HasClaim("IsAdmin", "True");
        }

        private readonly TournamentDbContext _db;

        //constructor to recieve injected DbContext

        public UserController(TournamentDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            //query the teams in the db, including their players and division
            var vm = _db.Team
                .Include(t => t.Division)
                .Select(t => new AdminIndexVm
                {
                    TeamId = t.TeamId,
                    TeamName =t.TeamName,
                    DivisionId = t.DivisionId,
                    DivisionName = t.Division.DivisionName,
                    RegistrationPaid = t.RegistrationPaid,
                    PaymentDate = t.PaymentDate

                }).ToList();

            return View(vm);
        }

        //GET index??? do we need an index page for the user controller???
        //skipping for now but can add later if needed
        //placeholder


        //get data for division and province dropdowns
        //not sure if we will do the dropdowns this way, commenting out for now
        //can come back later if needed
        //private IEnumerable<SelectListItem> BuildCategoryOptions()
        //{
        //    var options = new List<SelectListItem>();
        //    var divisions = _db.Division.ToList();

        //    foreach (var d in divisions)
        //    {
        //        options.Add(new SelectListItem
        //        {
        //            Value = d.DivisionName,
        //            Text = d.DivisionName
        //        });
        //    }
        //}


        //GET add
        [HttpGet]
        public IActionResult Add()
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

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

        //POST add TEAM
        [HttpPost]
        public IActionResult Add(TeamRegisterVm vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

            if (vm == null)
            {
                return NotFound();
            }
            //checking to make sure teamname doesnt already exist
            if (_db.Team.Any(t => t.TeamName == vm.TeamName))
            {
                ModelState.AddModelError("TeamName", "This team name already exists, please try another one");
            }

            //checking to make sure 4 player info was provided
            if (vm.Players == null || vm.Players.Count != 4)
            {
                ModelState.AddModelError("Players", "You must register exactly 4 players for your team");
            }

            //check if any of the validation has failed and redisplay the form
            if (!ModelState.IsValid)
            {
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
                RegistrationPaid = vm.RegistrationPaid,
                PaymentDate = vm.RegistrationPaid ? (vm.PaymentDate ?? DateTime.Now) : null
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
            TempData["Success"] = "Team has been added";

            return RedirectToAction("Index", "User");
        }

    



        //POST add player
        [HttpPost]
        public IActionResult AddPlayer(UserVm vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

            if (!ModelState.IsValid)
            {
                //dropdowns ?? come back to this later
                //vm.DivisionOptions = BuildCategoryOptions();
                //vm.ProvinceOptions = BuildProvinceOptions();
                return View(vm);
            }

            //create new player object:
            Player player = new Player();
            player.PlayerName = vm.PlayerName;
            player.City = vm.City;
            player.Province = vm.Province;
            player.Email = vm.Email;
            player.Phone = vm.Phone;

            _db.Player.Add(player);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }




        //need to do methods for editing and deleting teams and players 


    }
}
