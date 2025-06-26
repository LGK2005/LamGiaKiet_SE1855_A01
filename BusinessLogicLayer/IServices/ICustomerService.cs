using DataAccessLayer;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface ICustomerService
    {
        Task<OperationResult<List<Customer>>> GetAllCustomersAsync();
        Task<OperationResult<Customer>> GetCustomerByIdAsync(int id);
        Task<OperationResult> AddCustomerAsync(Customer customer);
        Task<OperationResult> UpdateCustomerAsync(Customer customer);
        Task<OperationResult> DeleteCustomerAsync(int id);
        Task<OperationResult<List<Customer>>> SearchCustomersAsync(string keyword);
        Task<OperationResult<Customer>> LoginByPhoneAsync(string phone);
        
        // Synchronous version for ViewModels
        OperationResult<Customer> GetCustomerById(int id);
        OperationResult UpdateCustomer(Customer customer);
    }
}
