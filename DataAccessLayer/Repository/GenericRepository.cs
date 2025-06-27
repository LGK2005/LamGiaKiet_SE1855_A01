using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private readonly string _connectionString;
        public GenericRepository(string connectionString)
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AS01_SalesData"]?.ConnectionString
                            ?? throw new InvalidOperationException("Connection string not found.");
        }

        public async Task<OperationResult> AddAsync<T>(T entity) where T : class
        {
            try
            {

                return await Task.Run(() =>
                {
                    using (var context = new AS01_SalesDataDataContext(_connectionString))
                    {
                        context.GetTable<T>().InsertOnSubmit(entity);
                        context.SubmitChanges();
                        return OperationResult.Ok();
                    }
                });
            }
            catch (Exception ex)
            {
                return OperationResult.Fail(ex.Message);
            }
        }

        public async Task<OperationResult> DeleteAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var context = new AS01_SalesDataDataContext(_connectionString))
                    {
                        var table = context.GetTable<T>();

                        // The predicate can be directly translated to a SQL WHERE clause by LINQ to SQL
                        var entity = table.SingleOrDefault(predicate);

                        if (entity != null)
                        {
                            table.DeleteOnSubmit(entity);
                            context.SubmitChanges();
                            return OperationResult.Ok();
                        }

                        return OperationResult.Fail("Entity not found for the given criteria.");
                    }
                });
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("A database error occurred during the delete operation.");
            }
        }

        public async Task<OperationResult<List<T>>> GetAllAsync<T>() where T : class
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var context = new AS01_SalesDataDataContext(_connectionString))
                    {
                        var entities = context.GetTable<T>().ToList();
                        return OperationResult<List<T>>.OK(entities);
                    }
                });
            }
            catch (Exception ex)
            {
                return OperationResult<List<T>>.Fail("An error occurred while retrieving all entities: " + ex.Message);
            }
        }


        public async Task<OperationResult<T>> GetByIdAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var context = new AS01_SalesDataDataContext(_connectionString))
                    {
                        var table = context.GetTable<T>();
                        var entity = table.SingleOrDefault(predicate);
                        if (entity == null)
                            return OperationResult<T>.Fail("Entity not found for the given criteria.");

                        return OperationResult<T>.OK(entity);
                    }
                });
            }
            catch(Exception ex)
            {
                return OperationResult<T>.Fail("An error occurred while retrieving the entity: " + ex.Message);
            }
        }

        public async Task<OperationResult> UpdateAsync<T>(T entity, Expression<Func<T, bool>> predicate) where T : class
        {
            try
            {
                return await Task.Run(() =>
                {
                    using (var context = new AS01_SalesDataDataContext(_connectionString))
                    {
                        var table = context.GetTable<T>();
                        var existingEntity = table.SingleOrDefault(predicate);
                        if (existingEntity == null)
                            return OperationResult.Fail("Entity not found for the given criteria.");

                        foreach (var prop in typeof(T).GetProperties())
                        {
                            if (prop.CanWrite)
                            {
                                var newValue = prop.GetValue(entity);
                                prop.SetValue(existingEntity, newValue);
                            }
                        }

                        context.SubmitChanges();
                        return OperationResult.Ok();
                    }
                });
            }
            catch(Exception ex)
            {
                return OperationResult.Fail("An error occurred while updating the entity: " + ex.Message);
            }
        }
    }
}
