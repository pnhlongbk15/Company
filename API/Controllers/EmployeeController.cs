using Business.Models;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController(ILogic<EmployeeModel> logicBusiness)
        {
            _logicBusiness = logicBusiness;
        }
        private readonly ILogic<EmployeeModel> _logicBusiness;

        [HttpGet]
        public IActionResult GetAllEmployee()
        {
            try
            {
                var aEmployee = _logicBusiness.GetAll();
                return Ok(aEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployeeById(string id)
        {
            try
            {
                var mEmployee = _logicBusiness.GetOneById(id);
                return Ok(mEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult AddOne([FromBody] EmployeeModel mEmployee)
        {
            try
            {
                _logicBusiness.AddOne(mEmployee);
                return Ok("Create new employee successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOne(string id)
        {
            try
            {
                _logicBusiness.DeleteOne(id);
                return Ok("Remove employee successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOne(string id, [FromBody] EmployeeModel mEmployee)
        {
            try
            {
                _logicBusiness.UpdateOne(id, mEmployee);
                return Ok("Update success.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
