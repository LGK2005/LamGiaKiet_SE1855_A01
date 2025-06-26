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
        public ProductService(string connectionString)
        {
            _productRepository = new ProductRepository(connectionString);
        }

        public async Task<OperationResult<List<Product>>> GetAllProductsAsync()
        {
            var result = await _productRepository.GetAllProductsAsync();
            return OperationResult<List<Product>>.OK(result.ToList());
        }

        public async Task<OperationResult<Product>> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            return product != null ? OperationResult<Product>.OK(product) : OperationResult<Product>.Fail("Not found");
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
            var all = await _productRepository.GetAllProductsAsync();
            var filtered = all.Where(p =>
                (!string.IsNullOrEmpty(p.ProductName) && p.ProductName.Contains(keyword))
            ).ToList();
            return OperationResult<List<Product>>.OK(filtered);
        }
    }
}
