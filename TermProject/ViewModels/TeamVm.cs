using System.ComponentModel.DataAnnotations;

namespace TermProject.ViewModels
{
    public class TeamVm
    {

        public int Id { get; set; }
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string Division { get; set; }

        public List<PlayerVm> Players { get; set; }

        public bool RegistrationPaid { get; set; }
        public DateTime? PaymentDate { get; set; }

    }


}
