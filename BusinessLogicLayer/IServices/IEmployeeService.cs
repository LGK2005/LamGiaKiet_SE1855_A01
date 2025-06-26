using DataAccessLayer;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IEmployeeService
    {
        Task<OperationResult<List<Employee>>> GetAllEmployeesAsync();
        Task<OperationResult<Employee>> GetEmployeeByIdAsync(int id);
        Task<OperationResult> AddEmployeeAsync(Employee employee);
        Task<OperationResult> UpdateEmployeeAsync(Employee employee);
        Task<OperationResult> DeleteEmployeeAsync(int id);
        Task<OperationResult<Employee>> LoginAsync(string username, string password);
        Task<OperationResult<List<Employee>>> SearchEmployeesAsync(string keyword);
    }
}
