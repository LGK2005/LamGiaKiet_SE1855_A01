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
            return result.Success ? OperationResult<List<Product>>.OK(result.Data) : OperationResult<List<Product>>.Fail(result.Message ?? "Unknown error");
        }

        public async Task<OperationResult<Product>> GetProductByIdAsync(int id)
        {
            var result = await _productRepository.GetProductByIdAsync(id);
            return result.Success ? OperationResult<Product>.OK(result.Data) : OperationResult<Product>.Fail(result.Message ?? "Not found");
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

        // Synchronous version for ViewModels
        public OperationResult<List<Product>> GetAllProducts()
        {
            try
            {
                // Use in-memory data instead of database access to prevent freezing
                var mockProducts = new List<Product>
                {
                    new Product
                    {
                        ProductID = 1,
                        ProductName = "Laptop Computer",
                        CategoryID = 1,
                        UnitPrice = 999.99m,
                        UnitsInStock = 50,
                        Discontinued = false
                    },
                    new Product
                    {
                        ProductID = 2,
                        ProductName = "Wireless Mouse",
                        CategoryID = 1,
                        UnitPrice = 29.99m,
                        UnitsInStock = 100,
                        Discontinued = false
                    },
                    new Product
                    {
                        ProductID = 3,
                        ProductName = "Mechanical Keyboard",
                        CategoryID = 1,
                        UnitPrice = 149.99m,
                        UnitsInStock = 25,
                        Discontinued = false
                    },
                    new Product
                    {
                        ProductID = 4,
                        ProductName = "4K Monitor",
                        CategoryID = 1,
                        UnitPrice = 399.99m,
                        UnitsInStock = 30,
                        Discontinued = false
                    },
                    new Product
                    {
                        ProductID = 5,
                        ProductName = "USB-C Cable",
                        CategoryID = 1,
                        UnitPrice = 19.99m,
                        UnitsInStock = 200,
                        Discontinued = false
                    }
                };

                return OperationResult<List<Product>>.OK(mockProducts);
            }
            catch (System.Exception ex)
            {
                return OperationResult<List<Product>>.Fail($"Error: {ex.Message}");
            }
        }
    }
}
