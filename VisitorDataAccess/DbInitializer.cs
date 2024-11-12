using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorDataAccess.Entities;

namespace VisitorDataAccess
{
    public class DbInitializer
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed companies
            modelBuilder.Entity<Company>().HasData(
                new Company { Id = 1, Name = "On Core" },
                new Company { Id = 2, Name = "Integration Team" }
            );

            // Seed employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, Name = "Angelo Dejaeghere", CompanyId = 1 },
                new Employee { Id = 2, Name = "Niels Stubbe", CompanyId = 1 },
                new Employee { Id = 3, Name = "Mathias Schaemelhout", CompanyId = 1 },
                new Employee { Id = 4, Name = "Nathan Moerman", CompanyId = 2 },
                new Employee { Id = 5, Name = "Robert Maes", CompanyId = 2 },
                new Employee { Id = 6, Name = "Nils Van Butsel", CompanyId = 2 }
            );
        }
    }
}
