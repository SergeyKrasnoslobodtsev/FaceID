using FaceID.Core.Entities;
using FaceID.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceID.Core.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository repository;
        public EmployeeService(IGenericRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Employee> AddAsync(Employee employee)
        {
           return await repository.AddAsync<Employee>(employee);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var spec = new Specifications.Employee.EmployeeWithNameAndPhotoSpecification();
            return await repository.ListAsync(spec);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await repository.GetByIdAsync<Employee>(id);
        }
    }
}
