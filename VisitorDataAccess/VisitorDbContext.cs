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

        //TODO SoftDelete
        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker.Entries()
        //        .Where(e => e.State == EntityState.Deleted);

        //    foreach (var entry in entries)
        //    {
        //        entry.State = EntityState.Modified;
        //        var entity = entry.Entity;
        //        var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
        //        if (isDeletedProperty != null)
        //        {
        //            isDeletedProperty.SetValue(entity, true);
        //        }
        //    }

        //    return base.SaveChanges();
        //}
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

            // Configure soft delete for entities
            modelBuilder.Entity<Company>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.IsDeleted);
            modelBuilder.Entity<Visitor>().HasQueryFilter(v => !v.IsDeleted);
            //modelBuilder.Entity<Visit>().HasQueryFilter(v => v.EndTime == null || v.EndTime > DateTime.Now);
            //modelBuilder.Entity<VisitorLog>().HasQueryFilter(vl => vl.TimeSpent > TimeSpan.Zero);

            // Define keys
            modelBuilder.Entity<Company>().HasKey(c => c.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.Id);
            modelBuilder.Entity<Visitor>().HasKey(v => v.Id);
            modelBuilder.Entity<Visit>().HasKey(v => v.Id);
            modelBuilder.Entity<VisitorLog>().HasKey(vl => vl.Id);
            modelBuilder.Entity<Admin>().HasKey(a => a.Id);

            // Define string lengths and optional EndTime
            modelBuilder.Entity<Company>().Property(c => c.Name).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Visitor>().Property(v => v.Company).HasMaxLength(50).IsRequired(false);
            modelBuilder.Entity<Employee>().Property(e => e.Name).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Visit>().Property(v => v.EndTime).IsRequired(false);

            //Apart class to seed data
            DbInitializer.SeedData(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}
