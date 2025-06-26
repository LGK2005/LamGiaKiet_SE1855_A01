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
            return OperationResult<List<Employee>>.OK(result.ToList());
        }

        public async Task<OperationResult<Employee>> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return employee != null ? OperationResult<Employee>.OK(employee) : OperationResult<Employee>.Fail("Not found");
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
            var all = await _employeeRepository.GetAllEmployeesAsync();
            var emp = all.FirstOrDefault(e => e.UserName == username && e.Password == password);
            if (emp == null)
                return OperationResult<Employee>.Fail("Invalid credentials");
            return OperationResult<Employee>.OK(emp);
        }

        public async Task<OperationResult<List<Employee>>> SearchEmployeesAsync(string keyword)
        {
            var all = await _employeeRepository.GetAllEmployeesAsync();
            var filtered = all.Where(e =>
                (!string.IsNullOrEmpty(e.Name) && e.Name.Contains(keyword)) ||
                (!string.IsNullOrEmpty(e.UserName) && e.UserName.Contains(keyword))
            ).ToList();
            return OperationResult<List<Employee>>.OK(filtered);
        }
    }
}
