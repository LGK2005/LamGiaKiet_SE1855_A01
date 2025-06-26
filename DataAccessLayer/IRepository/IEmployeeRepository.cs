using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IEmployeeRepository
    {
        Task<OperationResult<List<Employee>>> GetAllEmployeesAsync();
        Task<OperationResult<Employee>> GetEmployeeByIdAsync(int id);
        Task<OperationResult> AddEmployeeAsync(Employee employee);
        Task<OperationResult> UpdateEmployeeAsync(Employee employee);
        Task<OperationResult> DeleteEmployeeAsync(int id);
    }
}
