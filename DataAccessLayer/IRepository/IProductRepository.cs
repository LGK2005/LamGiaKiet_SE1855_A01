using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IProductRepository
    {
        Task<OperationResult<List<Product>>> GetAllProductsAsync();
        Task<OperationResult<Product>> GetProductByIdAsync(int id);
        Task<OperationResult> AddProductAsync(Product product);
        Task<OperationResult> UpdateProductAsync(Product product);
        Task<OperationResult> DeleteProductAsync(int id);
    }
}
