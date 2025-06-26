using DataAccessLayer;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IOrderService
    {
        Task<OperationResult<List<Order>>> GetAllOrdersAsync();
        Task<OperationResult<Order>> GetOrderByIdAsync(int id);
        Task<OperationResult> AddOrderAsync(Order order);
        Task<OperationResult> UpdateOrderAsync(Order order);
        Task<OperationResult> DeleteOrderAsync(int id);
        Task<OperationResult<List<Order>>> SearchOrdersAsync(string keyword);
        
        // Customer-specific methods
        OperationResult<List<Order>> GetOrdersByCustomerId(int customerId);
        OperationResult<int> CreateOrder(Order order, List<CartItem> orderDetails);
    }
}
