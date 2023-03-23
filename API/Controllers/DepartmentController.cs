using Business.Models;
using Business.Repositories.Interfaces;
using Dapper;
using Data.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
                var aEmployee = await _logicBusiness.GetAll();
                return Ok(aEmployee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                using (var connection = new SqlConnection("Data Source=LAPTOP-QD1OPRSA\\SQLEXPRESS;Integrated Security=true;Initial Catalog=Company;TrustServerCertificate=True;Encrypt=False"))
                {
                    Console.WriteLine(connection.ConnectionString);
                    var sql = @"SELECT Name,e.Id, e.FirstName, e.LastName, e.Email 
                        FROM Departments as d
                        INNER JOIN Employees as e
                        ON e.DepartmentId = d.Id";
                    var result = connection.Query<Department, Employee, Department>(sql, (department, employee) =>
                    {
                        department.Employees.Add(employee);
                        return department;
                    }, splitOn: "Id");

                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
