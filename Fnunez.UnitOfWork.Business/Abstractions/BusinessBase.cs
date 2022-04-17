using System.Linq.Expressions;
using Fnunez.UnitOfWork.Data.Abstractions;
using Fnunez.UnitOfWork.Models.Abstractions;

namespace Fnunez.UnitOfWork.Business.Abstractions;

public abstract class BusinessBase<R, M> where M : ModelBase where R : GenericRepository<M>
{
    protected IUnitOfWork UnitOfWork { get; private set; }
    protected R Repository { get; private set; }

    public BusinessBase(IUnitOfWork unitOfWork)
    {
        UnitOfWork = unitOfWork;
        Repository = UnitOfWork.GetRepository<R>();
    }

    public virtual void Delete(M entity)
    {
        Repository.Delete(entity);
        Repository.SaveChanges();
    }

    public async Task DeleteAsync(M entity)
    {
        await Repository.DeleteAsync(entity);
        await Repository.SaveChangesAsync();
    }

    public virtual void DeleteById(int id)
    {
        Repository.DeleteById(id);
        Repository.SaveChanges();
    }

    public virtual async Task DeleteByIdAsync(int id)
    {
        await Repository.DeleteByIdAsync(id);
        await Repository.SaveChangesAsync();
    }

    public IEnumerable<M> Get(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties)
    {
        return Repository.Get(filter, orderBy, skip, take, disableTracking, includedProperties).ToList();
    }

    public async Task<IEnumerable<M>> GetAsync(Expression<Func<M, bool>> filter = null, Func<IQueryable<M>, IOrderedQueryable<M>> orderBy = null, int? skip = null, int? take = null, bool disableTracking = true, params string[] includedProperties)
    {
        return await Repository.GetAsync(filter, orderBy, skip, take, disableTracking, includedProperties);
    }

    public virtual void HardDelete(M entity)
    {
        Repository.HardDelete(entity);
        Repository.SaveChanges();
    }

    public virtual void HardDeleteById(int id)
    {
        Repository.HardDeleteById(id);
        Repository.SaveChanges();
    }

    public virtual void Insert(M entity)
    {
        Repository.Insert(entity);
        Repository.SaveChanges();
    }

    public async virtual Task InsertAsync(M entity)
    {
        await Repository.InsertAsync(entity);
        await Repository.SaveChangesAsync();
    }

    public virtual void InsertRange(IEnumerable<M> entities)
    {
        Repository.InsertRange(entities);
        Repository.SaveChanges();
    }

    public async virtual Task InsertRangeAsync(IEnumerable<M> entities)
    {
        await Repository.InsertRangeAsync(entities);
        await Repository.SaveChangesAsync();
    }

    public virtual void Update(M entity)
    {
        Repository.Update(entity);
        Repository.SaveChanges();
    }

    public async virtual Task UpdateAsync(M entity)
    {
        await Repository.UpdateAsync(entity);
        await Repository.SaveChangesAsync();
    }

    public virtual void UpdateRange(IEnumerable<M> entities)
    {
        Repository.UpdateRange(entities);
        Repository.SaveChanges();
    }

    public async virtual Task UpdateRangeAsync(IEnumerable<M> entities)
    {
        await Repository.UpdateRangeAsync(entities);
        await Repository.SaveChangesAsync();
    }
}