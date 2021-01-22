using MyCompanyAPI.DTOs;
using MyCompanyAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompanyAPI.Repositories.IRepository
{
    public interface IEmployeeRepo
    {
        Employee AddEmployee(EmployeeDTO employee);
        Employee GetEmployee(int employeeID);

        IEnumerable<Employee> GetAllEmployees();
        Employee UpdateEmployee(Employee employee);
    }
}
