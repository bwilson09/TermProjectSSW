namespace TermProject.Models
{
    public class Division
    {
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }

        public List<Team> Teams { get; set; }
        public Division() { }
    }
}
