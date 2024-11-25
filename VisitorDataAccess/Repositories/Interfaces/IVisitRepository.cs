using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorDataAccess.Entities;

namespace VisitorDataAccess.Repositories.Interfaces
{
    public interface IVisitRepository : IGenericRepository<Visit>
    {
        Task<Visit?> GetActiveVisitByVisitorAsync(long visitorId);
        Task UpdateVisitAsync(Visit visit);
    }
}
