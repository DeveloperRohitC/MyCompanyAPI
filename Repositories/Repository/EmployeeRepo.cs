using AutoMapper;
using MyCompanyAPI.Context;
using MyCompanyAPI.DTOs;
using MyCompanyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyCompanyAPI.Repositories.IRepository
{

    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly CompanyDBContext _dbcontext = null;
        private readonly IMapper _mapper;
        public EmployeeRepo(CompanyDBContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public Employee AddEmployee(EmployeeDTO employee)
        {
            Employee empResult = null;
            try
            {
                var empModel = _mapper.Map<Employee>(employee);
                var data = _dbcontext.Employee.Add(empModel);
                _dbcontext.SaveChanges();
                empResult = empModel;
            }
            catch (Exception ex)
            {

            }
            return empResult;
        }

        public Employee GetEmployee(int employeeID)
        {
            return _dbcontext.Employee.FirstOrDefault(d => d.EmployeeID == employeeID) as Employee;
        }

        public IEnumerable<Employee> GetAllEmployees()
        {
            return _dbcontext.Employee.ToList<Employee>();
        }

        public Employee UpdateEmployee(Employee employee)
        {
            Employee empResult = null;
            try
            {
                var empTempResult = _dbcontext.Employee.FirstOrDefault(e => e.EmployeeID == employee.EmployeeID);
                if (empTempResult != null)
                {
                    empTempResult.EmployeeName = employee.EmployeeName;
                    empTempResult.Department = employee.Department;
                    empTempResult.DateOfJoining = employee.DateOfJoining;
                    empTempResult.PhotoFileName = employee.PhotoFileName;
                    _dbcontext.SaveChanges();
                    empResult = empTempResult;
                }
            }
            catch (Exception ex)
            {

            }
            return empResult;
        }
    }
}