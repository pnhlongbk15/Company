using AutoMapper;
using Business.Models;
using Business.Repositories.Interfaces;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Repositories
{
    public class EmployeeLogic : ILogic<EmployeeModel>
    {
        public EmployeeLogic(IEntityService<Employee> service, IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _service = service;
            _cache = cache;
        }
        private readonly IMemoryCache _cache;
        private IEntityService<Employee> _service;
        private IMapper _mapper;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public async Task<IEnumerable<EmployeeModel>> GetAll()
        {
            try
            {
                if (_cache.TryGetValue<IEnumerable<EmployeeModel>>("getAllEmployee", out var employees))
                {
                    return employees;
                }
                else
                {
                    try
                    {
                        await semaphore.WaitAsync();
                        if (_cache.TryGetValue("getAllEmployee", out employees))
                        {
                            return employees;
                        }
                        employees = _mapper.Map<IEnumerable<EmployeeModel>>(await _service.GetAll());
                        _cache.Set("getAllEmployee", employees);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                    return employees;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeModel> GetOneById(string id)
        {
            try
            {
                if (_cache.TryGetValue<IEnumerable<EmployeeModel>>("getAllEmployee", out var employees))
                {
                    var employee = employees.Single(x => x.Id == id);
                    if (employee != null)
                    {
                        return employee;
                    }
                }

                var result = await _service.GetOneById(id);
                if (result == null)
                {
                    throw new Exception("Invalid Id");
                }

                return _mapper.Map<EmployeeModel>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddOne(EmployeeModel model)
        {
            try
            {
                var eEmployee = _mapper.Map<Employee>(model);
                await _service.AddOne(eEmployee);
                _cache.Remove("getAllEmployee");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteOne(string id)
        {
            try
            {
                var eEmployee = await _service.GetOneById(id);
                if (eEmployee == null)
                {
                    throw new Exception("Employee doesn't exist.");
                }
                await _service.DeleteOne(eEmployee);
                _cache.Remove("getAllEmployee");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateOne(EmployeeModel model)
        {
            try
            {
                /*
                var eEmployee = await _service.GetOneById(id);
                if (eEmployee == null)
                {
                    throw new Exception("The Employee record couldn't be found.");
                }
                */
                await _service.UpdateOne(_mapper.Map<Employee>(model));
                _cache.Remove("getAllEmployee");
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
                await _service.DeleteOneByProcedure(email, departmentName);
                _cache.Remove("getAllEmployee");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
