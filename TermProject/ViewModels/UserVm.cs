using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TermProject.ViewModels
{
    public class UserVm
    {
        //may not need this one? used teamregisterVm for public and admin
        //team validation

        [Required(ErrorMessage = "A team name is required.")]
        [StringLength(50, ErrorMessage = "Team name must be 50 characters or fewer.")]
        public string TeamName { get; set; }

        [Required(ErrorMessage = "Please make a selection.")] //dropdown???????
        public string Division { get; set; }

        //not sure if this is required????? not going to validate yet
        public decimal RegistrationFee { get; set; }

        //default to false unless admin says otherwise (no validation needed??)
        public bool RegistrationPaid { get; set; } = false;


        [Required(ErrorMessage = "Payment date is required.")]
        public DateTime PaymentDate { get; set; }


        //player validation

        [Required(ErrorMessage = "Player name is required.")]
        [StringLength(50, ErrorMessage = "Player name must be 50 characters or fewer.")]
        public string PlayerName { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(50, ErrorMessage = "City must be 50 characters or fewer.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Please select a province")]
        //dropdown?????????????
        public string Province { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string Phone { get; set; }


        //dropdown options!!!!!!
        [ValidateNever]
        public IEnumerable<SelectListItem> DivisionOptions { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> ProvinceOptions { get; set; }

    }
}
