using Microsoft.AspNetCore.Mvc.Rendering;

namespace TermProject.ViewModels
{
    public class FilterVm
    {
        public int? DivisionId { get; set; }
        public bool? RegistrationPaid { get; set; }

        public List<SelectListItem> Divisions { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Payments { get; set; } = new List<SelectListItem>();

        public List<TeamVm> Teams { get; set; }
    }
}
