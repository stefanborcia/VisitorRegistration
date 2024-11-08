using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;

namespace VisitorDataAccess
{
    public class VisitorDbContext : DbContext
    {
        public VisitorDbContext(DbContextOptions<VisitorDbContext> options) : base(options){}
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VisitorLog> VisitorLogs { get; set; }
        public DbSet<Admin> Admins { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Visit>()
                .HasOne(v => v.AppointmentWith)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); // Specify 'Restrict' to avoid cascade cycles

            modelBuilder.Entity<Visit>()
                .HasOne(v => v.VisitingCompany)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);

            // Seed companies 
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "On Core" },
                new Company { Id = 2, Name = "Integration Team" }
            );

            // Seed employees 
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Angelo Dejaeghere", CompanyId = 1 },  
                new Employee { Id = 2, Name = "Niels Stubbe", CompanyId = 1 },        
                new Employee { Id = 3, Name = "Mathias Schaemelhout", CompanyId = 1 }         
            );

            // Seed employees 
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 4, Name = "Nathan Moerman", CompanyId = 2 },     
                new Employee { Id = 5, Name = "Robert Maes", CompanyId = 2 },     
                new Employee { Id = 6, Name = "Nils Van Butsel", CompanyId = 2 }     
            );
        }
    }
}
