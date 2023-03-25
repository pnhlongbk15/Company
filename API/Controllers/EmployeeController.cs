using AutoMapper;
using Business.Models;
using Business.Repositories.Interfaces;
using Data.Domain;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(ILogic<EmployeeModel> logicBusiness, CompanyContext context, IMapper mapper)
        {
            _logicBusiness = logicBusiness;
            _context = context;
            _mapper = mapper;
        }
        private readonly ILogic<EmployeeModel> _logicBusiness;
        private readonly CompanyContext _context;
        private readonly IMapper _mapper;

        [HttpGet]
        public async Task<IActionResult> GetAllEmployee()
        {
            try
            {
                var aEmployee = await _logicBusiness.GetAll();
                return Ok(aEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            try
            {
                var mEmployee = await _logicBusiness.GetOneById(id);
                return Ok(mEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] EmployeeModel mEmployee)
        {
            try
            {
                await _logicBusiness.AddOne(mEmployee);
                return Ok("Create new employee successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOneByProceduce([FromBody] EmployeeModel mEmployee)
        {
            try
            {
                mEmployee.Id = Guid.NewGuid().ToString();

                _context.Employees.Add(_mapper.Map<Employee>(mEmployee));
                return Ok(_context.SaveChanges());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOne(string Email, string DepartmentName)
        {
            try
            {
                await _logicBusiness.DeleteOneByProcedure(Email, DepartmentName);
                return Ok("Remove employee successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOne([FromBody] EmployeeModel mEmployee)
        {
            try
            {
                await _logicBusiness.UpdateOne(mEmployee);
                return Ok("Update success.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
