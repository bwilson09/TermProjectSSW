namespace TermProject.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName  { get; set; }
        public int DivisionId { get; set; }
        public bool RegistrationPaid { get; set; }
       
        public DateTime PaymentDate { get; set; }

        public Team() { }

        public Division Division { get; set; }
    }
}
