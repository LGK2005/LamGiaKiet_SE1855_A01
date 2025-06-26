using BusinessLogicLayer.IServices;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer;

namespace BusinessLogicLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;
        public EmployeeService(string connectionString)
        {
            _employeeRepository = new EmployeeRepository(connectionString);
        }

        public async Task<OperationResult<List<Employee>>> GetAllEmployeesAsync()
        {
            var result = await _employeeRepository.GetAllEmployeesAsync();
            return result.Success ? OperationResult<List<Employee>>.OK(result.Data) : OperationResult<List<Employee>>.Fail(result.Message ?? "Unknown error");
        }

        public async Task<OperationResult<Employee>> GetEmployeeByIdAsync(int id)
        {
            var result = await _employeeRepository.GetEmployeeByIdAsync(id);
            return result.Success ? OperationResult<Employee>.OK(result.Data) : OperationResult<Employee>.Fail(result.Message ?? "Not found");
        }

        public async Task<OperationResult> AddEmployeeAsync(Employee employee)
        {
            var validation = ValidationHelper.ValidateEmployee(employee);
            if (!validation.Success) return validation;
            return await _employeeRepository.AddEmployeeAsync(employee);
        }

        public async Task<OperationResult> UpdateEmployeeAsync(Employee employee)
        {
            var validation = ValidationHelper.ValidateEmployee(employee);
            if (!validation.Success) return validation;
            return await _employeeRepository.UpdateEmployeeAsync(employee);
        }

        public async Task<OperationResult> DeleteEmployeeAsync(int id)
        {
            return await _employeeRepository.DeleteEmployeeAsync(id);
        }

        public async Task<OperationResult<Employee>> LoginAsync(string username, string password)
        {
            var allResult = await _employeeRepository.GetAllEmployeesAsync();
            if (!allResult.Success) return OperationResult<Employee>.Fail(allResult.Message ?? "Error");
            var emp = allResult.Data.FirstOrDefault(e => e.UserName == username && e.Password == password);
            if (emp == null)
                return OperationResult<Employee>.Fail("Invalid credentials");
            return OperationResult<Employee>.OK(emp);
        }

        public async Task<OperationResult<List<Employee>>> SearchEmployeesAsync(string keyword)
        {
            var allResult = await _employeeRepository.GetAllEmployeesAsync();
            if (!allResult.Success) return OperationResult<List<Employee>>.Fail(allResult.Message ?? "Error");
            var filtered = allResult.Data.Where(e =>
                (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(keyword)) ||
                (!string.IsNullOrEmpty(e.UserName) && e.UserName.Contains(keyword))
            ).ToList();
            return OperationResult<List<Employee>>.OK(filtered);
        }
    }
}
