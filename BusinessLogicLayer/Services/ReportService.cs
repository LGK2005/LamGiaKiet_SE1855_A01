﻿using BusinessLogicLayer.IServices;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class ReportService : IReportService
    {
        private readonly OrderRepository _orderRepository;
        public ReportService(string connectionString)
        {
            _orderRepository = new OrderRepository(connectionString);
        }

        public async Task<OperationResult<List<Order>>> GetOrdersByPeriodAsync(DateTime from, DateTime to)
        {
            var allResult = await _orderRepository.GetAllOrdersAsync();
            if (!allResult.Success) return OperationResult<List<Order>>.Fail(allResult.Message ?? "Error");
            var filtered = allResult.Data.Where(o => o.OrderDate >= from && o.OrderDate <= to)
                              .OrderByDescending(o => o.OrderDate)
                              .ToList();
            return OperationResult<List<Order>>.OK(filtered);
        }
    }
}
