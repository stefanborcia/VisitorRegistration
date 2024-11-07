using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
