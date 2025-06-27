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
    public class ProductService : IProductService
    {
        private readonly ProductRepository _productRepository;
        
        public ProductService()
        {
            _productRepository = new ProductRepository();
        }
        
        public ProductService(string connectionString)
        {
            _productRepository = new ProductRepository(connectionString);
        }

        public async Task<OperationResult<List<Product>>> GetAllProductsAsync()
        {
            var result = await _productRepository.GetAllProductsAsync();
            return result;
        }

        public async Task<OperationResult<Product>> GetProductByIdAsync(int id)
        {
            var result = await _productRepository.GetProductByIdAsync(id);
            return result;
        }

        public async Task<OperationResult> AddProductAsync(Product product)
        {
            var validation = ValidationHelper.ValidateProduct(product);
            if (!validation.Success) return validation;
            return await _productRepository.AddProductAsync(product);
        }

        public async Task<OperationResult> UpdateProductAsync(Product product)
        {
            var validation = ValidationHelper.ValidateProduct(product);
            if (!validation.Success) return validation;
            return await _productRepository.UpdateProductAsync(product);
        }

        public async Task<OperationResult> DeleteProductAsync(int id)
        {
            return await _productRepository.DeleteProductAsync(id);
        }

        public async Task<OperationResult<List<Product>>> SearchProductsAsync(string keyword)
        {
            var allResult = await _productRepository.GetAllProductsAsync();
            if (!allResult.Success) return OperationResult<List<Product>>.Fail(allResult.Message ?? "Error");
            var filtered = allResult.Data.Where(p =>
                (!string.IsNullOrEmpty(p.ProductName) && p.ProductName.Contains(keyword))
            ).ToList();
            return OperationResult<List<Product>>.OK(filtered);
        }

        // Synchronous version for ViewModels - now uses database
        public OperationResult<List<Product>> GetAllProducts()
        {
            try
            {
                // Use the async method synchronously to get real database data
                var task = GetAllProductsAsync();
                task.Wait(); // Wait for the async operation to complete
                return task.Result;
            }
            catch (System.Exception ex)
            {
                return OperationResult<List<Product>>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
