using Dapper;
using Data.Domain;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Data.Services
{
    public class EmployeeService : IEntityService<Employee>
    {
        public EmployeeService(CompanyContext context, IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("Company"));
            _context = context;
        }
        private readonly SqlConnection _connection;
        private readonly CompanyContext _context;

        public async Task<IEnumerable<Employee>> GetAll()
        {
            try
            {

                var sql = @"SELECT e.Id, e.FirstName, e.LastName, e.Email, d.Name
                        FROM Employees as e
                        INNER JOIN Departments as d
                        ON e.DepartmentId = d.Id";

                var employees = await _connection.QueryAsync<Employee, Department, Employee>(sql, (employee, department) =>
                {
                    employee.DepartmentId = department.Name;
                    return employee;
                }, splitOn: "Name");

                return employees;

                //return _context.Employees.AsNoTracking().ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<Employee> GetOneById(string id)
        {
            try
            {
                return _context.Employees.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task AddOne(Employee entity)
        {
            try
            {
                entity.Id = Guid.NewGuid().ToString();
                _context.Employees.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task UpdateOne(Employee entity)
        {
            try
            {
                //_context.Employees.Entry(mEntity).CurrentValues.SetValues(entity);
                _context.Employees.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteOne(Employee entity)
        {
            try
            {
                _context.Employees.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteOneByProcedure(string email, string departmentName)
        {
            try
            {
                _connection.Query("Employees_Delete",
                                new { Email = email, DepartmentName = departmentName },
                                commandType: CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<dynamic> GetAllTest()
        {
            throw new NotImplementedException();
        }
    }
}
