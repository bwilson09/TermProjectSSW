using Microsoft.EntityFrameworkCore;

namespace TermProject.Models
{
    public class TournamentDbContext: DbContext
    {
        public TournamentDbContext(DbContextOptions<TournamentDbContext> options)
            : base(options) { }


        //confirm databse table names in db
        public DbSet<Team> Team => Set<Team>();

        public DbSet<Player> Player => Set<Player>();

        public DbSet<BowlingUser> BowlingUser => Set<BowlingUser>();



        //for dropdown???? commenting out for now 
        //public DbSet<Division> Division => Set<Division>();

        //force singular table name?? - again double check db names
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<BowlingUser>().ToTable("BowlingUser");
        }
    }
}
