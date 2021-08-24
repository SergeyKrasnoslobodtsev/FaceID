using FaceID.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FaceID.Core.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<Employee> AddAsync(Employee employee);
    }
}
