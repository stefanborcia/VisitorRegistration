﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorBusinessLogic.Services.Interfaces;
using VisitorDataAccess.Entities;
using VisitorDataAccess.Repositories;
using VisitorDataAccess.Repositories.Interfaces;
using VisitorDTOs;

namespace VisitorBusinessLogic.Services
{
    public class EmployeeServiceGeneric : IEmployeeServiceGeneric
    {
        private readonly IGenericRepository<Employee> _repository;
        private readonly IGenericRepository<Company> _companyRepository;

        public EmployeeServiceGeneric(
            IGenericRepository<Employee> repository,
            IGenericRepository<Company> companyRepository)
        {
            _repository = repository;
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetEmployeesAsync()
        {
            var employees = await _repository.GetAllRecordsAsync();
            return employees.Select(e => new EmployeeDTO
            {
                Id = e.Id,
                Name = e.Name,
                CompanyId = e.CompanyId 
            });
        }

        public async Task<EmployeeDTO> GetEmployeeByIdAsync(long id)
        {
            var employee = await _repository.GetRecordsByIdAsync(id);

            if (employee == null)
                return null;

            var employeeDto = new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                CompanyId = employee.CompanyId  
            };

            return employeeDto;
        }

        public async Task<EmployeeDTO> AddEmployeeAsync(long companyId, EmployeeDTO employeeDto)
        {
            var company = await _companyRepository.GetRecordsByIdAsync(companyId);
            if (company == null)
                throw new KeyNotFoundException($"Company with ID {companyId} does not exist.");

            var employee = new Employee
            {
                Name = employeeDto.Name,
                CompanyId = companyId
            };

            await _repository.AddRecordsAsync(employee);

            employeeDto.Id = employee.Id;  

            return employeeDto;
        }


        public async Task UpdateEmployeeAsync(EmployeeDTO employeeDto)
        {
            var company = await _companyRepository.GetRecordsByIdAsync(employeeDto.CompanyId);
            if (company == null)
                throw new KeyNotFoundException($"Company with ID {employeeDto.CompanyId} does not exist.");

            var employee = new Employee
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                CompanyId = employeeDto.CompanyId
            };

            await _repository.UpdateRecordsAsync(employee);
        }

        public async Task DeleteEmployeeAsync(long id)
        {
            await _repository.DeleteRecordsAsync(id);
        }
    }
}
