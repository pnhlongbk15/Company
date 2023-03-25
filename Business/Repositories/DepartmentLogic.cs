using AutoMapper;
using Business.Models;
using Business.Repositories.Interfaces;
using Data.Domain.Entities;
using Data.Services.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace Business.Repositories
{
    public class DepartmentLogic : ILogic<DepartmentModel>
    {
        public DepartmentLogic(IEntityService<Department> service, IMapper mapper, IMemoryCache cache)
        {
            _mapper = mapper;
            _service = service;
            _cache = cache;
        }
        private readonly IMemoryCache _cache;
        private IEntityService<Department> _service;
        private IMapper _mapper;
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public async Task<IEnumerable<DepartmentModel>> GetAll()
        {
            try
            {
                if (_cache.TryGetValue<IEnumerable<DepartmentModel>>("getAllDepartment", out var departments))
                {
                    return departments;
                }
                else
                {
                    try
                    {
                        await semaphore.WaitAsync();
                        if (_cache.TryGetValue("getAllDepartment", out departments))
                        {
                            return departments;
                        }
                        departments = _mapper.Map<IEnumerable<DepartmentModel>>(await _service.GetAll());
                        _cache.Set("getAllDepartment", departments);
                    }
                    finally { semaphore.Release(); }
                    return departments;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<DepartmentModel> GetOneById(string id)
        {
            try
            {
                if (_cache.TryGetValue<IEnumerable<DepartmentModel>>("getAllDepartment", out var departments))
                {
                    var department = departments.Single(x => x.Id == id);
                    if (department != null)
                    {
                        return department;
                    }
                }
                var result = await _service.GetOneById(id);
                if (result == null)
                {
                    throw new Exception("Invalid Id");
                }
                return _mapper.Map<DepartmentModel>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task AddOne(DepartmentModel model)
        {
            try
            {
                var eDepartment = _mapper.Map<Department>(model);
                await _service.AddOne(eDepartment);
                _cache.Remove("getAllDepartment");
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
                var eDepartment = await _service.GetOneById(id);
                if (eDepartment == null)
                {
                    throw new Exception("Department doesn't exist.");
                }
                await _service.DeleteOne(eDepartment);
                _cache.Remove("getAllDepartment");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateOne(DepartmentModel model)
        {
            try
            {
                /*
                var eDepartment = await _service.GetOneById(id);
                if (eDepartment == null)
                {
                    throw new Exception("The Employee record couldn't be found.");
                }
                */
                await _service.UpdateOne(_mapper.Map<Department>(model));
                _cache.Remove("getAllDepartment");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task DeleteOneByProcedure(string email, string departmentName)
        {
            throw new NotImplementedException();
        }
    }
}
