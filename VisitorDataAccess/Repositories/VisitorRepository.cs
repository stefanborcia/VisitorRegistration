﻿using Microsoft.EntityFrameworkCore;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;
using VisitorDTOs.VisitorDTO;
using Action = VisitorDataAccess.Entities.Action;

namespace VisitorDataAccess.Repositories
{
    public class VisitorRepository : IVisitorRepository
    {
        private readonly VisitorDbContext _dbContext;

        public VisitorRepository(VisitorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddVisitorAsync(Visitor visitor)
        {
            await _dbContext.Set<Visitor>().AddAsync(visitor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateVisitAsync(Visit visit)
        {
            await _dbContext.Set<Visit>().AddAsync(visit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Visitor?> GetVisitorByEmailAsync(string email)
        {
            return await _dbContext.Set<Visitor>().FirstOrDefaultAsync(v => v.Email == email);
        }

        public async Task<Visit?> GetActiveVisitByVisitorAsync(long visitorId)
        {
            return await _dbContext.Set<Visit>()
                .FirstOrDefaultAsync(v => v.Visitor != null && v.Visitor.Id == visitorId && v.EndTime == null);
        }

        public async Task UpdateVisitAsync(Visit visit)
        {
            _dbContext.Set<Visit>().Update(visit);
            await _dbContext.SaveChangesAsync();
        }

        public async Task CreateVisitorLogAsync(VisitorLog visitorLog)
        {
            _dbContext.VisitorLogs.Add(visitorLog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Visit>> GetActiveVisitsByEmployeeAsync(long employeeId)
        {
            return await _dbContext.Visits
                .Where(v => v.AppointmentWith != null && v.AppointmentWith.Id == employeeId && v.CurrentStatus == Action.SignIn)
                .ToListAsync();
        }

        public async Task AddRecordsAsync(Visit visit)
        {
            await _dbContext.Set<Visit>().AddAsync(visit);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<VisitorMonitoringDTO>> GetVisitorMonitoringAsync()
        {
            return await _dbContext.Visits
                .Select(e => new VisitorMonitoringDTO
                {
                    Id = e.Visitor.Id,
                    Name = e.Visitor.Name,
                    Company = e.Visitor.Company,
                    VisitingComapanyName = e.VisitingCompany.Name,
                    AppointmentWith = e.AppointmentWith.Name,
                    StartTime = e.StartTime

                })
                .ToListAsync();
        }
        public async Task<IEnumerable<VisitorRegistrationSearchDTO>> GetVisitorRegistrationSearchAsync(string search)
        {
            return await _dbContext.Visits
                .Where(e => e.Visitor.Id.ToString().Contains(search) 
                            || e.Visitor.Name.ToLower().Contains(search.ToLower()) 
                            || e.Visitor.Email.ToLower().Contains(search.ToLower()) 
                            || e.Visitor.Company.ToLower().Contains(search.ToLower()) 
                            || e.VisitingCompany.Name.ToLower().Contains(search.ToLower()) 
                            || e.AppointmentWith.Name.ToLower().Contains(search.ToLower()))

                //TODO implement search on date

                .Select(e => new VisitorRegistrationSearchDTO
                {
                    Id = e.Visitor.Id,
                    Name = e.Visitor.Name,
                    Email = e.Visitor.Email,
                    Company = e.Visitor.Company,
                    VisitingComapanyName = e.VisitingCompany.Name,
                    AppointmentWithEmployeeName = e.AppointmentWith.Name,
                    TimeSpent = e.EndTime.HasValue
                        ? (e.EndTime.Value - e.StartTime).ToString(@"hh\:mm\:ss")
                        : "In Progress",
                    StartTime = e.StartTime,
                })
                .ToListAsync(); 
        }
    }
}
