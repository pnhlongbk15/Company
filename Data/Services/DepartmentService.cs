using Dapper;
using Data.Domain;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Data.Services
{
    public class DepartmentService : IEntityService<Department>
    {
        public DepartmentService(IConfiguration configuration, CompanyContext context)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("Company"));
            _context = context;
        }
        private readonly SqlConnection _connection;
        private readonly CompanyContext _context;

        public async Task<IEnumerable<Department>> GetAll()
        {
            try
            {
                var sql = @"SELECT d.Id, d.Name, e.FirstName, e.LastName, e.Email 
                        FROM Departments as d
                        INNER JOIN Employees as e
                        ON e.DepartmentId = d.Id";

                var departments = await _connection.QueryAsync<Department, Employee, Department>(sql, (department, employee) =>
                {
                    department.Employees.Add(employee);
                    return department;
                }, splitOn: "FirstName");

                return departments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Department> GetOneById(string id)
        {
            try
            {
                var sql = @"SELECT Id, Name FROM Departments WHERE Id = @id;
                            SELECT * FROM Employees WHERE DepartmentId = @id;
                            ";
                using (var result = await _connection.QueryMultipleAsync(sql, new { id }))
                {
                    var department = await result.ReadFirstAsync<Department>();
                    var employee = await result.ReadAsync<Employee>();
                    employee.ToList().ForEach(e => department.Employees.Add(e));

                    return department;
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddOne(Department entity)
        {
            try
            {
                entity.Id = Guid.NewGuid().ToString();
                _context.Departments.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteOne(Department entity)
        {
            try
            {
                _context.Departments.Remove(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateOne(Department entity)
        {
            try
            {
                //_context.Departments.Entry(mEntity).CurrentValues.SetValues(entity);
                _context.Departments.Update(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
