using Microsoft.EntityFrameworkCore;
using Proiect_UrsuAlexandra.Models;

namespace Proiect_UrsuAlexandra.Data
{
    public class AgencyContext : DbContext
    {
        public AgencyContext(DbContextOptions<AgencyContext> options) :
       base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ListedJob> ListedJobs { get; set; }


        //modificam comportamentul implicit de a crea tabelele la plural
        //creem tabele cu denumirea la singular
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<Application>().ToTable("Application");
            modelBuilder.Entity<Job>().ToTable("Job");
            modelBuilder.Entity<Company>().ToTable("Company");
            modelBuilder.Entity<ListedJob>().ToTable("ListedJob");

            modelBuilder.Entity<ListedJob>()
            .HasKey(c => new { c.JobID, c.CompanyID });//configureaza cheia primara compusa
        }
    }
}
