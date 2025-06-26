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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly GenericRepository _genericRepository;
        private readonly string _connectionString;
        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
            _genericRepository = new GenericRepository(connectionString);
        }

        public Task<OperationResult<List<Category>>> GetAllCategoriesAsync()
            => _genericRepository.GetAllAsync<Category>();

        public Task<OperationResult<Category>> GetCategoryByIdAsync(int id)
            => _genericRepository.GetByIdAsync<Category>(c => c.CategoryID == id);

        public Task<OperationResult> AddCategoryAsync(Category category)
            => _genericRepository.AddAsync(category);

        public Task<OperationResult> UpdateCategoryAsync(Category category)
            => _genericRepository.UpdateAsync(category, c => c.CategoryID == category.CategoryID);

        public Task<OperationResult> DeleteCategoryAsync(int id)
            => _genericRepository.DeleteAsync<Category>(c => c.CategoryID == id);
    }
}
