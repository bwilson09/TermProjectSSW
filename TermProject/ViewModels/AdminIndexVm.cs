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

        //list of players associated with the team
        public List<PlayerRegisterVm> Players { get; set; }
    }
}
