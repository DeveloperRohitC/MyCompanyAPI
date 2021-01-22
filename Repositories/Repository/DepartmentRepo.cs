using AutoMapper;
using MyCompanyAPI.Context;
using MyCompanyAPI.DTOs;
using MyCompanyAPI.Models;
using MyCompanyAPI.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompanyAPI.Repositories.Repository
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly CompanyDBContext _dbcontext = null;
        private readonly IMapper _mapper;
        public DepartmentRepo(CompanyDBContext dbContext, IMapper mapper)
        {
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        public Department AddDepartment(DepartmentDTO department)
        {
            Department deptResult = null;
            try
            {
                var deptModel = _mapper.Map<Department>(department);
                var data = _dbcontext.Department.Add(deptModel);
                _dbcontext.SaveChanges();
                deptResult = deptModel;
            }
            catch (Exception ex)
            {

            }
            return deptResult;
        }

        public Department GetDepartment(int departmentID)
        {
            return _dbcontext.Department.FirstOrDefault(d => d.DepartmentID == departmentID) as Department;
        }

        public IEnumerable<Department> GetAllDepartments()
        {
            return _dbcontext.Department.ToList<Department>();
        }

        public Department UpdateDepartment(Department department)
        {
            Department deptResult = null;
            try
            {
                var deptTempResult = _dbcontext.Department.FirstOrDefault(d => d.DepartmentID == department.DepartmentID);
                if (deptTempResult != null)
                {
                    deptTempResult.DepartmentName = department.DepartmentName;
                    _dbcontext.SaveChanges();
                    deptResult = deptTempResult;
                }
            }
            catch (Exception ex)
            {

            }
            return deptResult;
        }
    }
}
