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

            var vm = new UserVm();

            //get data for dropdowns (come back to this later)
            //vm.DivisionOptions = BuildCategoryOptions();
            //vm.ProvinceOptions = BuildProvinceOptions();

            return View(vm);
        }

        //POST add TEAM
        [HttpPost]
        public IActionResult Add(UserVm vm)
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

            //create new team object:
            Team team = new Team();
            team.TeamName = vm.TeamName;
            team.Division.DivisionName = vm.Division;
            team.RegistrationPaid = vm.RegistrationPaid;
            team.PaymentDate = vm.PaymentDate;

            _db.Team.Add(team);
            _db.SaveChanges();
            return RedirectToAction("Index");
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
