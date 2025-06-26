using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly GenericRepository _genericRepository;
        private readonly string _connectionString;
        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
            _genericRepository = new GenericRepository(connectionString);
        }

        public ProductRepository()
        {
            _connectionString = "DefaultConnection";
            _genericRepository = new GenericRepository(_connectionString);
        }

        public Task<OperationResult<List<Product>>> GetAllProductsAsync()
            => _genericRepository.GetAllAsync<Product>();

        public Task<OperationResult<Product>> GetProductByIdAsync(int id)
            => _genericRepository.GetByIdAsync<Product>(p => p.ProductID == id);

        public Task<OperationResult> AddProductAsync(Product product)
            => _genericRepository.AddAsync(product);

        public Task<OperationResult> UpdateProductAsync(Product product)
            => _genericRepository.UpdateAsync(product, p => p.ProductID == product.ProductID);

        public Task<OperationResult> DeleteProductAsync(int id)
            => _genericRepository.DeleteAsync<Product>(p => p.ProductID == id);
    }
}
