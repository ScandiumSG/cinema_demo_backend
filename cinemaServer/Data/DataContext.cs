using cinemaServer.Models.PureModels;
using cinemaServer.Models.PureModels.People;
using cinemaServer.Models.User;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationships

            // Define composite key for screening
            modelBuilder.Entity<Screening>().HasKey(s => new { s.Id, s.MovieId, s.TheaterId});
            // Define relation to Movie
            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Movie)
                .WithMany()
                .HasForeignKey(s => s.MovieId);
            // Define relation to Theater
            modelBuilder.Entity<Screening>()
                .HasOne(s => s.Theater)
                .WithMany()
                .HasForeignKey(s => s.TheaterId);

            // Auto include for queries
            modelBuilder.Entity<Screening>().Navigation(s => s.Movie).AutoInclude();
            modelBuilder.Entity<Screening>().Navigation(s => s.Theater).AutoInclude();

            // Seed database
            DatabaseSeeder seeder = new DatabaseSeeder(4466222, 200, 30, 5000);
            modelBuilder.Entity<Movie>().HasData(seeder.Movies);
            modelBuilder.Entity<Theater>().HasData(seeder.Theaters);
            modelBuilder.Entity<Screening>().HasData(seeder.Screenings);

            modelBuilder.Entity<ApplicationUser>().HasData(seeder.GeneratePredefinedUsers());
        }

        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
