using System.ComponentModel.DataAnnotations;

namespace TermProject.ViewModels
{
    public class TeamVm
    {
        [Required]
        public string TeamName { get; set; }
        [Required]
        public string Division { get; set; }

        public List<string> Players { get; set; }
    }
}
