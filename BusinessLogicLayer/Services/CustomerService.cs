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
    public class CustomerService : ICustomerService
    {
        private readonly CustomerRepository _customerRepository;
        
        public CustomerService()
        {
            _customerRepository = new CustomerRepository();
        }
        
        public CustomerService(string connectionString)
        {
            _customerRepository = new CustomerRepository(connectionString);
        }

        public async Task<OperationResult<List<Customer>>> GetAllCustomersAsync()
        {
            var result = await _customerRepository.GetAllAsync<Customer>();
            return result.Success ? OperationResult<List<Customer>>.OK(result.Data) : OperationResult<List<Customer>>.Fail(result.Message ?? "Unknown error");
        }

        public async Task<OperationResult<Customer>> GetCustomerByIdAsync(int id)
        {
            var result = await _customerRepository.GetByIdAsync<Customer>(c => c.CustomerID == id);
            return result.Success ? OperationResult<Customer>.OK(result.Data) : OperationResult<Customer>.Fail(result.Message ?? "Not found");
        }

        public async Task<OperationResult> AddCustomerAsync(Customer customer)
        {
            var validation = ValidationHelper.ValidateCustomer(customer);
            if (!validation.Success) return validation;
            return await _customerRepository.AddAsync(customer);
        }

        public async Task<OperationResult> UpdateCustomerAsync(Customer customer)
        {
            var validation = ValidationHelper.ValidateCustomer(customer);
            if (!validation.Success) return validation;
            return await _customerRepository.UpdateAsync(customer, c => c.CustomerID == customer.CustomerID);
        }

        public async Task<OperationResult> DeleteCustomerAsync(int id)
        {
            return await _customerRepository.DeleteAsync<Customer>(c => c.CustomerID == id);
        }

        public async Task<OperationResult<List<Customer>>> SearchCustomersAsync(string keyword)
        {
            var allResult = await _customerRepository.GetAllAsync<Customer>();
            if (!allResult.Success) return OperationResult<List<Customer>>.Fail(allResult.Message ?? "Error");
            var filtered = allResult.Data.Where(c =>
                (!string.IsNullOrEmpty(c.CompanyName) && c.CompanyName.Contains(keyword)) ||
                (!string.IsNullOrEmpty(c.ContactName) && c.ContactName.Contains(keyword)) ||
                (!string.IsNullOrEmpty(c.Phone) && c.Phone.Contains(keyword))
            ).ToList();
            return OperationResult<List<Customer>>.OK(filtered);
        }

        public async Task<OperationResult<Customer>> LoginByPhoneAsync(string phone)
        {
            var customer = await _customerRepository.GetCustomerByPhoneAsync(phone);
            if (customer == null)
                return OperationResult<Customer>.Fail("Customer not found");
            return OperationResult<Customer>.OK(customer);
        }

        // Synchronous versions for ViewModels
        public OperationResult<Customer> GetCustomerById(int id)
        {
            try
            {
                var result = _customerRepository.GetByIdAsync<Customer>(c => c.CustomerID == id).Result;
                return result.Success ? OperationResult<Customer>.OK(result.Data) : OperationResult<Customer>.Fail(result.Message ?? "Not found");
            }
            catch (System.Exception ex)
            {
                return OperationResult<Customer>.Fail($"Error: {ex.Message}");
            }
        }

        public OperationResult UpdateCustomer(Customer customer)
        {
            try
            {
                var validation = ValidationHelper.ValidateCustomer(customer);
                if (!validation.Success) return validation;
                
                var result = _customerRepository.UpdateAsync(customer, c => c.CustomerID == customer.CustomerID).Result;
                return result;
            }
            catch (System.Exception ex)
            {
                return OperationResult.Fail($"Error: {ex.Message}");
            }
        }
    }
}
