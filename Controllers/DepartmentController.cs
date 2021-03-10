using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyCompanyAPI.Context;
using MyCompanyAPI.DTOs;
using MyCompanyAPI.Models;
using MyCompanyAPI.Repositories.IRepository;


namespace MyCompanyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[EnableCors("APICustomPolicy")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepo _dept;
        private readonly CompanyDBContext _dbcontext;
        private readonly IMapper _mapper;
        public DepartmentController(IDepartmentRepo dept, CompanyDBContext dbContext, IMapper mapper)
        {
            _dept = dept;
            _dbcontext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _dept.GetAllDepartments();
            if (result == null)
                return NotFound("No Data");
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _dept.GetDepartment(id);
            if (result == null)
                return NotFound("No Data Found");
            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] DepartmentDTO dept)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = _dept.AddDepartment(dept);
            return Ok(result);
        }

        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody] Department dept)
        {
            if (id != dept.DepartmentID)
                return BadRequest("Please provide Department ID");

            if (!ModelState.IsValid)
                return BadRequest("Invalid object sent from client.");

            var deptData = _dept.GetDepartment(id);

            if (deptData == null)
            {
                return BadRequest($"Department with id: {id}, hasn't been found in db.");
            }

            var result = _dept.UpdateDepartment(dept);
            return Ok(result);
        }
    }
}
