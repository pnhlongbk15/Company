using AutoMapper;
using Business.Models;
using Business.Repositories.Interfaces;
using Data.Domain.Entities;
using Data.Services.Interfaces;

namespace Business.Repositories
{
    public class DepartmentLogic : ILogic<DepartmentModel>
    {
        public DepartmentLogic(IEntityService<Department> service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        private IEntityService<Department> _service;
        private IMapper _mapper;
        public async Task<IEnumerable<DepartmentModel>> GetAll()
        {
            try
            {
                var departments = await _service.GetAll();
                return _mapper.Map<IEnumerable<DepartmentModel>>(departments);
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
                var department = await _service.GetOneById(id);
                if (department == null)
                {
                    throw new Exception("Invalid Id");
                }
                return _mapper.Map<DepartmentModel>(department);
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
