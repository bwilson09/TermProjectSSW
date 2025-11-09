namespace TermProject.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int TeamId { get; set; }

        public Player() { }
    }
}
