using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly GenericRepository _genericRepository;
        private readonly string _connectionString;
        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
            _genericRepository = new GenericRepository(connectionString);
        }

        public OrderRepository()
        {
            _connectionString = "DefaultConnection";
            _genericRepository = new GenericRepository(_connectionString);
        }

        public Task<OperationResult<List<Order>>> GetAllOrdersAsync()
            => _genericRepository.GetAllAsync<Order>();

        public Task<OperationResult<Order>> GetOrderByIdAsync(int id)
            => _genericRepository.GetByIdAsync<Order>(o => o.OrderID == id);

        public Task<OperationResult> AddOrderAsync(Order order)
            => _genericRepository.AddAsync(order);

        public Task<OperationResult> UpdateOrderAsync(Order order)
            => _genericRepository.UpdateAsync(order, o => o.OrderID == order.OrderID);

        public Task<OperationResult> DeleteOrderAsync(int id)
            => _genericRepository.DeleteAsync<Order>(o => o.OrderID == id);
    }
}
