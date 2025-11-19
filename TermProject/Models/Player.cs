namespace TermProject.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public int TeamId { get; set; }

        //navigation property: allows for admin to add players linked to a team
        //in POST action (user controller)

        public Team Team { get; set; }

        public Player() { }
    }
}
