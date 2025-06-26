using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IOrderRepository
    {
        Task<OperationResult<List<Order>>> GetAllOrdersAsync();
        Task<OperationResult<Order>> GetOrderByIdAsync(int id);
        Task<OperationResult> AddOrderAsync(Order order);
        Task<OperationResult> UpdateOrderAsync(Order order);
        Task<OperationResult> DeleteOrderAsync(int id);
    }
}
