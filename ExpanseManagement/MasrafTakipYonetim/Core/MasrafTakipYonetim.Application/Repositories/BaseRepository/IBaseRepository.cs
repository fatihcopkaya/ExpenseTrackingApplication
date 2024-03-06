using MasrafTakipYonetim.Domain.Entities.Common;
using System.Linq.Expressions;


namespace MasrafTakipYonetim.Application.Repositories.BaseRepository
{
    public interface IBaseRepository<T> where T : class, IBaseEntity, new()
    {
        Task<T> GetAsync(int id);
        Task<T> GetAsync(Expression<Func<T, bool>> filter = null, Expression<Func<T, object>> include = null);
        Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> filter = null, params Expression<Func<T, object>>[] includes);
        Task<IList<T>> GetListAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
    }
}
