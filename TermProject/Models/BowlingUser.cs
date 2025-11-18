namespace TermProject.Models
{
    public class BowlingUser
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public bool IsAdmin { get; set; }
    }
}
