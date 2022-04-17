using System.Linq.Expressions;
using Fnunez.UnitOfWork.Models.Abstractions;

namespace Fnunez.UnitOfWork.Data.Abstractions;

public interface IGenericRepository<M> : IDisposable where M : ModelBase
{
    int Count(Expression<Func<M, bool>> filter = null);
    Task<int> CountAsync(Expression<Func<M, bool>> filter = null);
    void Delete(M entity);
    Task DeleteAsync(M entity);
    void DeleteById(int id);
    Task DeleteByIdAsync(int id);
    void DeleteRange(IEnumerable<M> entities);
    Task DeleteRangeAsync(IEnumerable<M> entities);
    Task<IEnumerable<N>> ExecuteStoredProcedureValuesAsync<N>(string query, Dictionary<string, object> parameters = null, string connectionString = null, int? commandTimeOut = null);
    bool Exists(Expression<Func<M, bool>> filter);
    Task<bool> ExistsAsync(Expression<Func<M, bool>> filter);
    IEnumerable<M> Get(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties);
    Task<IEnumerable<M>> GetAsync(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties);
    M GetFirstOrDefault(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties);
    Task<M> GetFirstOrDefaultAsync(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties);
    M GetById(int id);
    Task<M> GetByIdAsync(int id);
    IQueryable<M> GetQuery(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includeProperties);
    void HardDelete(M entity);
    void HardDeleteById(int id);
    void Insert(M entity);
    Task InsertAsync(M entity);
    void InsertRange(IEnumerable<M> entities);
    Task InsertRangeAsync(IEnumerable<M> entities);
    void Update(M entity);
    Task UpdateAsync(M entity);
    void UpdateRange(IEnumerable<M> entities);
    Task UpdateRangeAsync(IEnumerable<M> entities);
    void Upsert(M entity);
    Task UpsertAsync(M entity);
    void SaveChanges();
    Task SaveChangesAsync();
}