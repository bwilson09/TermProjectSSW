namespace TermProject.Models
{
    public class Team
    {
        public int TeamId { get; set; }
        public string TeamName  { get; set; }
        public string Division { get; set; }
        public bool RegistrationPaid { get; set; }
        //DateOnly or DateTime??
        public DateOnly PaymentDate { get; set; }

        public Team() { }
    }
}
