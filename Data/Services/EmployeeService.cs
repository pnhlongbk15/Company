using Data.Domain;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Services
{
    public class EmployeeService : IEntityService<Employee>
    {
        public EmployeeService(CompanyContext context)
        {
            _context = context;
        }

        private readonly CompanyContext _context;

        public IEnumerable<Employee> GetAll()
        {
            try
            {
                return _context.Employees.AsNoTracking().ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public Employee GetOneById(string id)
        {
            try
            {
                return _context.Employees.FirstOrDefault(e => e.Id == id);
            }
            catch (Exception ex) { throw ex; }
        }

        public void AddOne(Employee entity)
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
        public void UpdateOne(Employee mEntity, Employee entity)
        {
            try
            {
                _context.Entry(mEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteOne(Employee entity)
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

    }
}
