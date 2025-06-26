using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface ICategoryRepository
    {
        Task<OperationResult<List<Category>>> GetAllCategoriesAsync();
        Task<OperationResult<Category>> GetCategoryByIdAsync(int id);
        Task<OperationResult> AddCategoryAsync(Category category);
        Task<OperationResult> UpdateCategoryAsync(Category category);
        Task<OperationResult> DeleteCategoryAsync(int id);
    }
}
