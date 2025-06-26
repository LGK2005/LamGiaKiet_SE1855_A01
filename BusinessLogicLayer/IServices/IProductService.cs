using DataAccessLayer;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IProductService
    {
        Task<OperationResult<List<Product>>> GetAllProductsAsync();
        Task<OperationResult<Product>> GetProductByIdAsync(int id);
        Task<OperationResult> AddProductAsync(Product product);
        Task<OperationResult> UpdateProductAsync(Product product);
        Task<OperationResult> DeleteProductAsync(int id);
        Task<OperationResult<List<Product>>> SearchProductsAsync(string keyword);
    }
}
