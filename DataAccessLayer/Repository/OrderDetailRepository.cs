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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly GenericRepository _genericRepository;
        private readonly string _connectionString;
        public OrderDetailRepository(string connectionString)
        {
            _connectionString = connectionString;
            _genericRepository = new GenericRepository(connectionString);
        }

        public Task<OperationResult<List<Order_Detail>>> GetAllOrderDetailsAsync()
            => _genericRepository.GetAllAsync<Order_Detail>();

        public Task<OperationResult<Order_Detail>> GetOrderDetailByIdAsync(int id)
            => _genericRepository.GetByIdAsync<Order_Detail>(od => od.OrderID == id);

        public Task<OperationResult> AddOrderDetailAsync(Order_Detail orderDetail)
            => _genericRepository.AddAsync(orderDetail);

        public Task<OperationResult> UpdateOrderDetailAsync(Order_Detail orderDetail)
            => _genericRepository.UpdateAsync(orderDetail, od => od.OrderID == orderDetail.OrderID && od.ProductID == orderDetail.ProductID);

        public Task<OperationResult> DeleteOrderDetailAsync(int id)
            => _genericRepository.DeleteAsync<Order_Detail>(od => od.OrderID == id);
    }
}
