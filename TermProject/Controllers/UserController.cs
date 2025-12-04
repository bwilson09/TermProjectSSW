using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TermProject.Models;
using TermProject.ViewModels;

namespace TermProject.Controllers
{
    [Authorize(Policy = "AdminOnly")]
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
            var teams = _db.Team
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

            //get the view model as the wrapper to get the teams and set up the dropdowns for filtering
            var vm = new AdminWrapperVm
            {
                Teams = teams,
                Filter = new FilterVm
                {
                    Divisions = _db.Division.Select(d => new SelectListItem
                    {
                        Value = d.DivisionId.ToString(),
                        Text = d.DivisionName
                    }).ToList(),

                    Payments = new List<SelectListItem>
                {
                        new SelectListItem { Value = "", Text = "-- All --" },
                new SelectListItem { Value = "true", Text = "Paid Teams" },
                new SelectListItem { Value = "false", Text = "Unpaid Teams" }
                }
                }
            };
            return View(vm);
        }



        // ADD TEAMS**********************

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
            if (_db.Team.Any(t => t.TeamName.ToLower() == vm.TeamName.ToLower()))
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


        // ADD PLAYERS **********************

        //POST add player
        [HttpPost]
        public IActionResult AddPlayer(UserVm vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

            if (_db.Team.Any(t => t.TeamName.ToLower() == vm.TeamName.ToLower()))
            {
                ModelState.AddModelError("TeamName", "This team name already exists, please try another one");
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
            TempData["Success"] = "Player(s) have been added";
            return RedirectToAction("Index");
        }









        // EDIT TEAMS**********************

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!IsAdmin())
            {
                //only admins can edit teams
                return RedirectToAction("Denied", "Auth");
            }


            // query to get the team with the specfieid ID
            //get division and player info
            var team = _db.Team
                .Include(t => t.Division)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            //map the team info to teamregistervm to populate values in the edit form
            var vm = new TeamRegisterVm
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                DivisionId = team.DivisionId,
                RegistrationPaid = team.RegistrationPaid,
                PaymentDate = team.PaymentDate,

                //map the players to the playerregisterVM for form
                Players = team.Players.Select(p => new PlayerRegisterVm
                {
                    PlayerName = p.PlayerName,
                    City = p.City,
                    Province = p.Province,
                    Email = p.Email,
                    Phone = p.Phone
                }).ToList(),

                //build the divisions dropdown
                Divisions = _db.Division
                    .Select(d => new SelectListItem
                    {
                        //default selection will be the current division
                        Value = d.DivisionId.ToString(),
                        Text = d.DivisionName
                    })
                    .ToList()
            };

            return View(vm);
        }



        [HttpPost]
        public IActionResult Edit(TeamRegisterVm vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

            //checking to make sure teamname doesnt already exist
            //if (_db.Team.Any(t => t.TeamName.ToLower() == vm.TeamName.ToLower()))
            //{
            //    ModelState.AddModelError("TeamName", "This team name already exists, please try another one");
            //}

            //wrapped in an if/else so that the team being edited is excluded from the check
            if (_db.Team.Any(t =>
                t.TeamId != vm.TeamId &&                
                t.TeamName.ToLower() == vm.TeamName.ToLower()))
            {
                ModelState.AddModelError("TeamName", "This team name already exists, please try another one");
            }

            //tell model to ignore player validation for this post 
            ModelState.Remove("Players");

            if (!ModelState.IsValid)
            {
                //if model state is invalid, repopulate divisions for dropdown and redisplay page
                vm.Divisions = _db.Division
                    .Select(d => new SelectListItem
                    {
                        Value = d.DivisionId.ToString(),
                        Text = d.DivisionName
                    })
                    .ToList();
                return View(vm);
            }

            //get the team from the database in order to update 
            var team = _db.Team
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == vm.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            //update the team entity with the edited values
            team.TeamName = vm.TeamName;
            team.DivisionId = (int)vm.DivisionId.Value;
            team.RegistrationPaid = vm.RegistrationPaid;
            team.PaymentDate = vm.RegistrationPaid ? (vm.PaymentDate ?? DateTime.Now) : null;


            _db.SaveChanges();
            TempData["Success"] = "Team has been updated.";
            return RedirectToAction("Index");
        }







        // MAANGE PLAYERS: ***********************

        [HttpGet]
        public IActionResult ManagePlayers(int id)
        {
            if (!IsAdmin())
            {
                //only admins can edit teams
                return RedirectToAction("Denied", "Auth");
            }

            // query to get the team with the specfieid ID
            //get division and player info

            var team = _db.Team
                .Include(t => t.Division)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            //map the team info to teamregistervm to populate values in the edit form
            var vm = new TeamRegisterVm
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                DivisionId = team.DivisionId,
                RegistrationPaid = team.RegistrationPaid,
                PaymentDate = team.PaymentDate,

                //map the players to the playerregisterVM for form
                Players = team.Players.Select(p => new PlayerRegisterVm
                {
                    PlayerName = p.PlayerName,
                    City = p.City,
                    Province = p.Province,
                    Email = p.Email,
                    Phone = p.Phone
                }).ToList(),

                //build the divisions dropdown
                Divisions = _db.Division
                    .Select(d => new SelectListItem
                    {
                        //default selection will be the current division
                        Value = d.DivisionId.ToString(),
                        Text = d.DivisionName
                    })
                    .ToList()
            };

            return View(vm);
        }



        [HttpPost]
        public IActionResult ManagePlayers(TeamRegisterVm vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }


            if (!ModelState.IsValid)
            {
                //if model state is invalid, repopulate divisions for dropdown and redisplay page
                vm.Divisions = _db.Division
                    .Select(d => new SelectListItem
                    {
                        Value = d.DivisionId.ToString(),
                        Text = d.DivisionName
                    })
                    .ToList();
                return View(vm);
            }

            //get the team from the database in order to update 
            var team = _db.Team
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == vm.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            //loop through all team players and update their info
            for (int i = 0; i < vm.Players.Count; i++)
            {
                team.Players[i].PlayerName = vm.Players[i].PlayerName;
                team.Players[i].City = vm.Players[i].City;
                team.Players[i].Province = vm.Players[i].Province;
                team.Players[i].Email = vm.Players[i].Email;
                team.Players[i].Phone = vm.Players[i].Phone;
            }

            _db.SaveChanges();
            TempData["Success"] = "Player(s) have successfully been updated";
            return RedirectToAction("Index");
        }






        // DELETE TEAMS**********************


        //view page to display team and player info to confirm user is sure
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

            //get the team and player values to be deleeted
            var team = _db.Team
                .Include(t => t.Division)
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == id);

            if (team == null)
            {
                return NotFound();
            }

            //map team and player info to adminindexVM to display
            var vm = new AdminIndexVm
            {
                TeamId = team.TeamId,
                TeamName = team.TeamName,
                DivisionName = team.Division.DivisionName,
                RegistrationPaid = team.RegistrationPaid,
                PaymentDate = team.PaymentDate,
                Players = team.Players.Select(p => new PlayerRegisterVm
                    {
                        PlayerName = p.PlayerName,
                        City = p.City,
                        Province = p.Province,
                        Email = p.Email,
                        Phone = p.Phone
                    }).ToList()
                  };

            return View(vm);
        }


        [HttpPost]
        public IActionResult Delete(AdminIndexVm vm)
        {
            if (!IsAdmin())
            {
                return RedirectToAction("Denied", "Auth");
            }

            //query to find the team and players to delete
            var team = _db.Team
                .Include(t => t.Players)
                .FirstOrDefault(t => t.TeamId == vm.TeamId);

            if (team == null)
            {
                return NotFound();
            }

            //Remove all players associated with the team (RemoveRange will delete a list)
            _db.Player.RemoveRange(team.Players);
            //delete the team itself
            _db.Team.Remove(team);
            _db.SaveChanges();
            TempData["Success"] = "Team has been deleted.";
            return RedirectToAction("Index");
        }









        // FILTERING TEAMS**********************

        [HttpGet]
        public IActionResult Filter(int? divisionId, bool? registrationPaid)
        {
            //get a new instance of the filter view model
            var vm = new AdminWrapperVm();

            vm.Filter.DivisionId = divisionId;
            vm.Filter.RegistrationPaid = registrationPaid;
            //build the divisions dropdown

            vm.Filter.Divisions = _db.Division
               .Select(d => new SelectListItem
               {
                   Value = d.DivisionId.ToString(),
                   Text = d.DivisionName
               })
               .ToList();
            

            //building the paid dropdown
                vm.Filter.Payments = new List<SelectListItem>
                {
                    new SelectListItem {Value = "", Text = "-- All --" },
                    new SelectListItem {Value = "true", Text = "Paid Teams" },
                    new SelectListItem {Value = "false", Text = "Unpaid Teams" }
                };

            var query = _db.Team.Include(t => t.Division).AsQueryable();
            //query to filter by division
            if(divisionId != null && divisionId.HasValue)
            {
                query = query.Where(t => t.DivisionId == divisionId.Value);
                
            }

            if(registrationPaid != null && registrationPaid.HasValue)
            {
                query = query.Where(t => t.RegistrationPaid == registrationPaid.Value);
                
            }

            vm.Teams = query.Select(t => new AdminIndexVm
            { 
                TeamId = t.TeamId,
                TeamName = t.TeamName,
                DivisionName = t.Division.DivisionName,
                RegistrationPaid = t.RegistrationPaid,
                PaymentDate = t.PaymentDate
            }).ToList();
             
            return View(vm);


        }


        // REGISTRATION PAGE ************************

        public IActionResult Registration()
        {
            var totalTeams = _db.Team.Count();
            var paidTeams = _db.Team.Count(t => t.RegistrationPaid);
            var totalFees = paidTeams * 200;

            var vm = new RegistrationVm
            {
                TotalTeams = totalTeams,
                PaidTeams = paidTeams,
                TotalFeesCollected = totalFees
            };

            return View(vm);
        }

















        
    }
}
