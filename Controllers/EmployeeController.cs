using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCompanyAPI.Context;
using MyCompanyAPI.DTOs;
using MyCompanyAPI.Models;
using MyCompanyAPI.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepo _emp;
        private readonly CompanyDBContext _dbcontext;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepo emp, CompanyDBContext dbContext, IMapper mapper)
        {
            _emp = emp;
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _emp.GetAllEmployees();
            if (result == null)
                return NotFound("No Data");
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _emp.GetEmployee(id);
            if (result == null)
                return NotFound("No Data Found");
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] EmployeeDTO empDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _emp.AddEmployee(empDTO);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Employee emp)
        {
            if (id != emp.EmployeeID)
                return BadRequest("Please provide employee ID");

            if (!ModelState.IsValid)
                return BadRequest("Invalid object sent from client.");

            var empData = _emp.GetEmployee(id);

            if (empData == null)
            {
                return BadRequest($"Employee with id: {id}, hasn't been found in db.");
            }

            var result= _emp.UpdateEmployee(emp);
            return Ok(result);
        }
    }
}

