using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IOrderDetailRepository
    {
        Task<OperationResult<List<Order_Detail>>> GetAllOrderDetailsAsync();
        Task<OperationResult<Order_Detail>> GetOrderDetailByIdAsync(int id);
        Task<OperationResult> AddOrderDetailAsync(Order_Detail orderDetail);
        Task<OperationResult> UpdateOrderDetailAsync(Order_Detail orderDetail);
        Task<OperationResult> DeleteOrderDetailAsync(int id);
    }
}
