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

        public Task<DepartmentModel> GetOneById(string id)
        {
            throw new NotImplementedException();
        }

        public void AddOne(DepartmentModel model)
        {
            throw new NotImplementedException();
        }

        public void DeleteOne(string id)
        {
            throw new NotImplementedException();
        }

        public void UpdateOne(string id, DepartmentModel model)
        {
            throw new NotImplementedException();
        }
    }
}
