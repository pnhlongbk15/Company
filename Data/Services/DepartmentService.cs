using Dapper;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Data.Services
{
    public class DepartmentService : IEntityService<Department>
    {
        public DepartmentService(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("Company"));
        }
        private readonly SqlConnection _connection;
        public async Task<IEnumerable<Department>> GetAll()
        {
            try
            {
                var sql = @"SELECT Name, e.FirstName, e.LastName, e.Email 
                        FROM Departments as d
                        INNER JOIN Employees as e
                        ON e.DepartmentId = d.Id";

                var departments = await _connection.QueryAsync<Department, Employee, Department>(sql, (department, employee) =>
                {
                    department.Employees.Add(employee);
                    return department;
                }, splitOn: "FirstName");
                /*
                departments.ToList().ForEach(department =>
                {
                    Console.WriteLine($"Department: {department.Name}");

                    department.Employees.ForEach(employee =>
                    {
                        Console.Write(employee.FirstName);
                    });
                    Console.WriteLine();

                });
                */
                return departments;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Department> GetOneById(string id)
        {
            throw new NotImplementedException();
        }

        public void AddOne(Department entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(Department entity)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne(Department mEntity, Department entity)
        {
            throw new NotImplementedException();
        }
    }
}
