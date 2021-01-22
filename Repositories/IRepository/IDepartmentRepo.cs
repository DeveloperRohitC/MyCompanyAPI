using MyCompanyAPI.DTOs;
using MyCompanyAPI.Models;
using System.Collections.Generic;

namespace MyCompanyAPI.Repositories.IRepository
{
    public interface IDepartmentRepo
    {
        Department AddDepartment(DepartmentDTO department);

        Department GetDepartment(int departmentID);

        IEnumerable<Department> GetAllDepartments();

        Department UpdateDepartment(Department department);
    }
}
