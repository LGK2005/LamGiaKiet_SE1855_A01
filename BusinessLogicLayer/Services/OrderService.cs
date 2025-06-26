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
    public class OrderService : IOrderService
    {
        private readonly OrderRepository _orderRepository;
        public OrderService(string connectionString)
        {
            _orderRepository = new OrderRepository(connectionString);
        }

        public async Task<OperationResult<List<Order>>> GetAllOrdersAsync()
        {
            var result = await _orderRepository.GetAllOrdersAsync();
            return result.Success ? OperationResult<List<Order>>.OK(result.Data) : OperationResult<List<Order>>.Fail(result.Message ?? "Unknown error");
        }

        public async Task<OperationResult<Order>> GetOrderByIdAsync(int id)
        {
            var result = await _orderRepository.GetOrderByIdAsync(id);
            return result.Success ? OperationResult<Order>.OK(result.Data) : OperationResult<Order>.Fail(result.Message ?? "Not found");
        }

        public async Task<OperationResult> AddOrderAsync(Order order)
        {
            var validation = ValidationHelper.ValidateOrder(order);
            if (!validation.Success) return validation;
            return await _orderRepository.AddOrderAsync(order);
        }

        public async Task<OperationResult> UpdateOrderAsync(Order order)
        {
            var validation = ValidationHelper.ValidateOrder(order);
            if (!validation.Success) return validation;
            return await _orderRepository.UpdateOrderAsync(order);
        }

        public async Task<OperationResult> DeleteOrderAsync(int id)
        {
            return await _orderRepository.DeleteOrderAsync(id);
        }

        public async Task<OperationResult<List<Order>>> SearchOrdersAsync(string keyword)
        {
            var allResult = await _orderRepository.GetAllOrdersAsync();
            if (!allResult.Success) return OperationResult<List<Order>>.Fail(allResult.Message ?? "Error");
            var filtered = allResult.Data.Where(o =>
                o.OrderID.ToString().Contains(keyword)
            ).ToList();
            return OperationResult<List<Order>>.OK(filtered);
        }
    }
}
