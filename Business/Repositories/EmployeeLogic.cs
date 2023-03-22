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

        public IEnumerable<EmployeeModel> GetAll()
        {
            try
            {
                var aEmployeee = _service.GetAll();
                return _mapper.Map<IEnumerable<EmployeeModel>>(aEmployeee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public EmployeeModel GetOneById(string id)
        {
            try
            {
                var eEmployee = _service.GetOneById(id);
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

        public void AddOne(EmployeeModel model)
        {
            try
            {
                var eEmployee = _mapper.Map<Employee>(model);
                _service.AddOne(eEmployee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteOne(string id)
        {
            try
            {
                var eEmployee = _service.GetOneById(id);
                if (eEmployee == null)
                {
                    throw new Exception("Employee doesn't exist.");
                }
                _service.DeleteOne(_mapper.Map<Employee>(eEmployee));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateOne(string id, EmployeeModel model)
        {
            try
            {
                var eEmployee = _service.GetOneById(id);
                if (eEmployee == null)
                {
                    throw new Exception("The Employee record couldn't be found.");
                }
                _service.UpdateOne(eEmployee, _mapper.Map<Employee>(model));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
