using cinemaServer.Models.PureModels;
using cinemaServer.Models.User;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Data
{
    public class DataContext : DbContext
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0290:Use primary constructor", Justification = "<Pending>")]
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
            // SCREENING
            // Define composite key for screening
            modelBuilder.Entity<Screening>().HasKey(s => new { s.Id, s.MovieId });
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
            modelBuilder.Entity<Screening>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Screening)
                .HasForeignKey(s => s.ScreeningId);
            modelBuilder.Entity<Screening>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd();

            // THEATER
            modelBuilder.Entity<Theater>()
                .HasMany(t => t.Seats)
                .WithOne(s => s.Theater)
                .HasForeignKey(t => t.TheaterId);
            // SEATS
            modelBuilder.Entity<Seat>().HasKey(s => new { s.Id, s.TheaterId });
            modelBuilder.Entity<Seat>()
                .HasOne(s => s.Theater)
                .WithMany(t => t.Seats)
                .HasForeignKey(s => s.TheaterId);
            modelBuilder.Entity<Seat>()
                .HasMany(s => s.Tickets)
                .WithOne(t => t.Seat);

            // TICKET
            modelBuilder.Entity<Ticket>().HasKey(t => t.Id);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Customer)
                .WithMany(c => c.Tickets)
                .HasForeignKey(t => t.CustomerId);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Screening)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => new { t.ScreeningId, t.MovieId });
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Seat)
                .WithMany(s => s.Tickets)
                .HasForeignKey(t => new { t.SeatId, t.TheaterId });

            // Auto include for queries
            modelBuilder.Entity<Screening>().Navigation(s => s.Movie).AutoInclude();
            modelBuilder.Entity<Screening>().Navigation(s => s.Theater).AutoInclude();
            modelBuilder.Entity<Screening>().Navigation(s => s.Tickets).AutoInclude();
            modelBuilder.Entity<Theater>().Navigation(t => t.Seats).AutoInclude();
            modelBuilder.Entity<Ticket>().Navigation(t => t.Seat).AutoInclude();
            modelBuilder.Entity<ApplicationUser>().Navigation(u => u.Tickets).AutoInclude();

            // Seed database
            DatabaseSeeder seeder = new DatabaseSeeder(123456, 20, 5, 50, 1000, 5000);
            modelBuilder.Entity<Movie>().HasData(seeder.Movies);
            modelBuilder.Entity<Theater>().HasData(seeder.Theaters);
            modelBuilder.Entity<Screening>().HasData(seeder.Screenings);

            modelBuilder.Entity<ApplicationUser>().HasData(seeder.Customers);
            modelBuilder.Entity<Ticket>().HasData(seeder.Tickets);

            modelBuilder.Entity<Seat>().HasData(seeder.Seats);
        }

        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
    }
}
