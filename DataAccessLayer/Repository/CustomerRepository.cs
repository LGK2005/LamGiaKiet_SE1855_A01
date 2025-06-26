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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly GenericRepository _genericRepository;
        private readonly string _connectionString;
        public CustomerRepository(string connectionString)
        {
            _connectionString = connectionString;
            _genericRepository = new GenericRepository(connectionString);
        }

        public CustomerRepository()
        {
            _connectionString = "DefaultConnection";
            _genericRepository = new GenericRepository(_connectionString);
        }

        // CRUD operations using GenericRepository
        public Task<OperationResult<List<T>>> GetAllAsync<T>() where T : class
            => _genericRepository.GetAllAsync<T>();

        public Task<OperationResult<T>> GetByIdAsync<T>(Expression<Func<T, bool>> predicate) where T : class
            => _genericRepository.GetByIdAsync<T>(predicate);

        public Task<OperationResult> AddAsync<T>(T entity) where T : class
            => _genericRepository.AddAsync<T>(entity);

        public Task<OperationResult> UpdateAsync<T>(T entity, Expression<Func<T, bool>> predicate) where T : class
            => _genericRepository.UpdateAsync<T>(entity, predicate);

        public Task<OperationResult> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
            => _genericRepository.DeleteAsync<T>(predicate);

        // Custom method: Get customer by phone (for customer login)
        public async Task<Customer?> GetCustomerByPhoneAsync(string phone)
        {
            var result = await _genericRepository.GetByIdAsync<Customer>(c => c.Phone == phone);
            return result.Success ? result.Data : null;
        }
    }
}
