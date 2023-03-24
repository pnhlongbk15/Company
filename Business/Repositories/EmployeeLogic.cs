using AutoMapper;
using Business.Models;
using Business.Repositories.Interfaces;
using Data.Domain.Entities;
using Data.Services.Interfaces;

namespace Business.Repositories
{
    public class EmployeeLogic : ILogic<EmployeeModel>
    {
        public EmployeeLogic(IEntityService<Employee> service, IMapper mapper)
        {
            _mapper = mapper;
            _service = service;
        }
        private IEntityService<Employee> _service;
        private IMapper _mapper;

        public async Task<IEnumerable<EmployeeModel>> GetAll()
        {
            try
            {
                var aEmployeee = await _service.GetAll();
                return _mapper.Map<IEnumerable<EmployeeModel>>(aEmployeee);
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
                var eEmployee = await _service.GetOneById(id);
                if (eEmployee == null)
                {
                    throw new Exception("Invalid Id");
                }
                var employee = _mapper.Map<EmployeeModel>(eEmployee);

                return employee;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
