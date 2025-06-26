using DataAccessLayer;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IReportService
    {
        Task<OperationResult<List<Order>>> GetOrdersByPeriodAsync(DateTime from, DateTime to);
    }
}
