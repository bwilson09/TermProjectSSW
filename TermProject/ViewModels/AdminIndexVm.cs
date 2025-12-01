using System.Globalization;

namespace TermProject.ViewModels
{
    public class AdminIndexVm
    {
        
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public bool RegistrationPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
    }
}
