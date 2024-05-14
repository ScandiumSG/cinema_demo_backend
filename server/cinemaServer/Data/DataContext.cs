using cinemaServer.Models.PureModels;
using cinemaServer.Models.PureModels.People;
using Microsoft.EntityFrameworkCore;

namespace cinemaServer.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Screening> Screenings { get; set; }
        public DbSet<Theater> Theaters { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DatabaseSeeder seeder = new DatabaseSeeder(44662272, 100);
            modelBuilder.Entity<Movie>().HasData(seeder.Movies);
        }
    }
}
