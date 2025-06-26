using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IGenericRepository
    {
        Task<OperationResult<List<T>>> GetAllAsync<T>() where T : class;
        Task<OperationResult<T>> GetByIdAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<OperationResult> AddAsync<T>(T entity) where T : class;
        Task<OperationResult> UpdateAsync<T>(T entity, Expression<Func<T, bool>> predicate) where T : class;
        Task<OperationResult> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
    }
}
