using Business.Models;
using Business.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DepartmentController : ControllerBase
    {
        public DepartmentController(ILogic<DepartmentModel> logicBusiness)
        {
            _logicBusiness = logicBusiness;
        }
        private readonly ILogic<DepartmentModel> _logicBusiness;

        [HttpGet]
        public async Task<IActionResult> GetAllDepartment()
        {
            try
            {
                var aDepartments = await _logicBusiness.GetAll();
                return Ok(aDepartments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartmentById(string id)
        {
            try
            {
                var department = await _logicBusiness.GetOneById(id);
                return Ok(department);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOne([FromBody] DepartmentModel mDepartment)
        {
            try
            {
                await _logicBusiness.AddOne(mDepartment);
                return Ok("Create new department successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOne(string id)
        {
            try
            {
                await _logicBusiness.DeleteOne(id);
                return Ok("Remove department successful.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOne([FromBody] DepartmentModel mDepartment)
        {
            try
            {
                await _logicBusiness.UpdateOne(mDepartment);
                return Ok("Update success.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
