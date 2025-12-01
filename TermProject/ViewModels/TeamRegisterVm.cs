using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TermProject.ViewModels
{
    public class TeamRegisterVm
    {
        //vm for the team info
        [Required(ErrorMessage = "Division is required")]
        public int? DivisionId { get; set; }

        [Required(ErrorMessage = "Team name is required")]
        [StringLength(50, ErrorMessage = "Team name must be 50 characters or fewer.")]
        public string TeamName { get; set; }

   

        [Required]
        [MinLength(4, ErrorMessage ="Must have 4 players register")]
        public List<PlayerRegisterVm>Players { get; set; }

        public bool RegistrationPaid { get; set; }
        public DateTime? PaymentDate { get; set; }

        //adding list here in order to populate dropdown on user page with the division names
        public IEnumerable<SelectListItem> Divisions { get; set; } = new List<SelectListItem>();
    }

    public class PlayerRegisterVm
    {
        //vm for the player info
        //made seperately to help enforce the 4 player rule
        //seperate the team vs player info for access
        //more reusable if need to edit a player
        [Required(ErrorMessage = "Player name is required")]
        public string PlayerName { get; set; }

        [Required(ErrorMessage ="City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Province is required")]
        [StringLength(2, MinimumLength =2, ErrorMessage ="Province must be 2 letter code")]
        public string Province { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage ="Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [Phone(ErrorMessage ="Invalid phone number format")]
        public string Phone { get; set; }
    }
}

