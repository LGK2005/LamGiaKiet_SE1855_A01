using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly GenericRepository _genericRepository;
        private readonly string _connectionString;
        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
            _genericRepository = new GenericRepository(connectionString);
        }

        public Task<OperationResult<List<Employee>>> GetAllEmployeesAsync()
            => _genericRepository.GetAllAsync<Employee>();

        public Task<OperationResult<Employee>> GetEmployeeByIdAsync(int id)
            => _genericRepository.GetByIdAsync<Employee>(e => e.EmployeeID == id);

        public Task<OperationResult> AddEmployeeAsync(Employee employee)
            => _genericRepository.AddAsync(employee);

        public Task<OperationResult> UpdateEmployeeAsync(Employee employee)
            => _genericRepository.UpdateAsync(employee, e => e.EmployeeID == employee.EmployeeID);

        public Task<OperationResult> DeleteEmployeeAsync(int id)
            => _genericRepository.DeleteAsync<Employee>(e => e.EmployeeID == id);
    }
}
