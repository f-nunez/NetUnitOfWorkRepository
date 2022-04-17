using System.Data;
using System.Linq.Expressions;
using Dapper;
using Fnunez.UnitOfWork.Models.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Fnunez.UnitOfWork.Data.Abstractions;

public class GenericRepository<M> : IGenericRepository<M> where M : ModelBase
{
    protected string ConnectionString { get; private set; }
    protected DbContext DbContext { get; private set; }
    protected DbSet<M> DbSet { get; private set; }
    protected bool IsDisposed { get; private set; }

    public GenericRepository(DbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<M>();
        IsDisposed = false;
    }

    public virtual int Count(Expression<Func<M, bool>> filter = null)
    {
        IQueryable<M> query = DbSet;
        if (filter != null)
        {
            query.Where(filter);
        }

        return query.Count();
    }

    public virtual async Task<int> CountAsync(Expression<Func<M, bool>> filter = null)
    {
        IQueryable<M> query = DbSet;
        if (filter != null)
        {
            query.Where(filter);
        }

        return await query.CountAsync();
    }

    public void Delete(M entity)
    {
        entity.IsActive = false;
        Update(entity);
    }

    public async Task DeleteAsync(M entity)
    {
        entity.IsActive = false;
        await UpdateAsync(entity);
    }

    public void DeleteById(int id)
    {
        Delete(GetById(id));
    }

    public async Task DeleteByIdAsync(int id)
    {
        await DeleteAsync(await GetByIdAsync(id));
    }

    public void DeleteRange(IEnumerable<M> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsActive = false;
        }

        UpdateRange(entities);
    }

    public async Task DeleteRangeAsync(IEnumerable<M> entities)
    {
        foreach (var entity in entities)
        {
            entity.IsActive = false;
        }

        await UpdateRangeAsync(entities);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<IEnumerable<N>> ExecuteStoredProcedureValuesAsync<N>(string query, Dictionary<string, object> parameters = null, string connectionString = null, int? commandTimeOut = null)
    {
        using IDbConnection connection = GetConnection(connectionString);
        connection.Open();
        if (!(parameters?.Any() ?? false))
        {
            return await connection.QueryAsync<N>($"{query}", commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut);
        }

        return await connection.QueryAsync<N>($"{query}", GetDynamicParameters(parameters), commandType: CommandType.StoredProcedure, commandTimeout: commandTimeOut);
    }

    public bool Exists(Expression<Func<M, bool>> filter)
    {
        return GetQuery(filter).Any();
    }

    public async Task<bool> ExistsAsync(Expression<Func<M, bool>> filter)
    {
        return await GetQuery(filter).AnyAsync();
    }

    public IEnumerable<M> Get(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties)
    {
        return GetQuery(filter, orderBy, skip, take, disableTracking, includedProperties).ToList<M>();
    }

    public async Task<IEnumerable<M>> GetAsync(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties)
    {
        return await GetQuery(filter, orderBy, skip, take, disableTracking, includedProperties).ToListAsync<M>();
    }

    public M GetById(int id)
    {
        return DbSet.Find(id);
    }

    public async Task<M> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public M GetFirstOrDefault(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties)
    {
        return GetQuery(filter, orderBy, skip, take, disableTracking, includedProperties).FirstOrDefault<M>();
    }

    public async Task<M> GetFirstOrDefaultAsync(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties)
    {
        return await GetQuery(filter, orderBy, skip, take, disableTracking, includedProperties).FirstOrDefaultAsync<M>();
    }

    public virtual IQueryable<M> GetQuery(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includeProperties)
    {
        IQueryable<M> query = DbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        if (skip.HasValue && take.HasValue)
        {
            query = query.Skip(skip.Value).Take(take.Value);
        }

        if (disableTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public void HardDelete(M entity)
    {
        if (DbContext.Entry(entity).State == EntityState.Detached)
        {
            DbSet.Attach(entity);
        }

        DbSet.Remove(entity);
    }

    public void HardDeleteById(int id)
    {
        HardDelete(GetById(id));
    }

    public void Insert(M entity)
    {
        entity.IsActive = true;
        DbContext.Add(entity);
    }

    public async Task InsertAsync(M entity)
    {
        entity.IsActive = true;
        await DbContext.AddAsync(entity);
    }

    public void InsertRange(IEnumerable<M> entities)
    {
        foreach (M entity in entities)
        {
            entity.IsActive = true;
        }

        DbContext.AddRange(entities);
    }

    public async Task InsertRangeAsync(IEnumerable<M> entities)
    {
        foreach (M entity in entities)
        {
            entity.IsActive = true;
        }

        await DbContext.AddRangeAsync(entities);
    }

    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }

    public async Task SaveChangesAsync()
    {
        await DbContext.SaveChangesAsync();
    }

    public void Update(M entity)
    {
        var entry = DbContext.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            var currentEntry = GetById(entity.Id);
            if (currentEntry != null)
            {
                var attachedEntry = DbContext.Entry(currentEntry);
                attachedEntry.CurrentValues.SetValues(entity);
                entry = attachedEntry;
            }
            else
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        if (entry.State == EntityState.Unchanged)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return;
        }

        if (entry.State == EntityState.Modified)
        {
            DbContext.SaveChanges();
        }
    }

    public async Task UpdateAsync(M entity)
    {
        var entry = DbContext.Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            var currentEntry = await GetByIdAsync(entity.Id);
            if (currentEntry != null)
            {
                var attachedEntry = DbContext.Entry(currentEntry);
                attachedEntry.CurrentValues.SetValues(entity);
                entry = attachedEntry;
            }
            else
            {
                DbSet.Attach(entity);
                DbContext.Entry(entity).State = EntityState.Modified;
            }
        }

        if (entry.State == EntityState.Unchanged)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            return;
        }

        if (entry.State == EntityState.Modified)
        {
            await DbContext.SaveChangesAsync();
        }
    }

    public void UpdateRange(IEnumerable<M> entities)
    {
        foreach (var entity in entities)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var currentEntry = GetById(entity.Id);
                if (currentEntry != null)
                {
                    var attachedEntry = DbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    DbSet.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }
                continue;
            }

            if (entry.State == EntityState.Unchanged)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                continue;
            }
        }

        DbContext.SaveChanges();
    }

    public async Task UpdateRangeAsync(IEnumerable<M> entities)
    {
        foreach (var entity in entities)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                var currentEntry = await GetByIdAsync(entity.Id);
                if (currentEntry != null)
                {
                    var attachedEntry = DbContext.Entry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    DbSet.Attach(entity);
                    DbContext.Entry(entity).State = EntityState.Modified;
                }
                continue;
            }
            if (entry.State == EntityState.Unchanged)
            {
                DbContext.Entry(entity).State = EntityState.Modified;
                continue;
            }
        }

        DbContext.SaveChanges();
    }

    public void Upsert(M entity)
    {
        var entry = GetById(entity.Id);
        if (entry == null)
        {
            Insert(entity);
            return;
        }

        Update(entity);
    }

    public async Task UpsertAsync(M entity)
    {
        var entry = await GetByIdAsync(entity.Id);
        if (entry == null)
        {
            await InsertAsync(entity);
            return;
        }

        await UpdateAsync(entity);
    }

    protected void Dispose(bool disposing)
    {
        if (!IsDisposed && disposing)
        {
            DbContext.Dispose();
        }

        IsDisposed = true;
    }

    public IDbConnection GetConnection(string connectionString = null)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            ConnectionString = DbContext.Database.GetDbConnection().ConnectionString;
            return new SqlConnection(ConnectionString);
        }

        return new SqlConnection(connectionString);
    }

    private DynamicParameters GetDynamicParameters(Dictionary<string, object> parameters)
    {
        var sqlParameters = new DynamicParameters();
        foreach (var pair in parameters)
        {
            sqlParameters.Add(pair.Key, pair.Value);
        }

        return sqlParameters;
    }
}