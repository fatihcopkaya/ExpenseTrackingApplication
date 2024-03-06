using MasrafTakipYonetim.Application.Repositories.BaseRepository;
using MasrafTakipYonetim.Domain.Entities;
using MasrafTakipYonetim.Domain.Entities.Common;
using MasrafTakipYönetim.Domain.Entities.Common;
using MasrafTakipYonetim.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MasrafTakipYonetim.Persistence.Repositories.BaseRepository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, IBaseEntity, new()
    {
        private readonly MasrafTakipYonetimDbContext _context;

        public BaseRepository(MasrafTakipYonetimDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
           await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, object>> include = null)
        {
            IQueryable<TEntity> queryable= _context.Set<TEntity>();

            if (filter !=null)
            {
                queryable = queryable.Where(filter);
            }

            if (include != null)
            {
                queryable = queryable.Include(include);
            }

            return await queryable.AsNoTracking().SingleOrDefaultAsync();
        }

        public async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] includes)
        {
           IQueryable<TEntity> queryable = _context.Set<TEntity>();

            if (filter != null)
            {
                queryable = queryable.Where(filter);
            }
            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                {
                    queryable = queryable.Include(include);

                }
            }
            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<IList<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
          IQueryable<TEntity> query= _context.Set<TEntity>();

            AppUser existingUser = new AppUser() ; // Mevcut bir AppUser örneği elde ettim.
            var entityId = existingUser.Id; // AppUser örneği ile Id'yi elde ettik.
            var entity = await _context.Set<TEntity>().FindAsync(entityId);

            //var entity = await _context.Set<TEntity>().FindAsync();


            if (filter!=null)
            {
                query = query.Where(filter);

                //entity.IsDeleted = true;
                //await _context.SaveChangesAsync();
             
            }

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy!=null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }
        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = _context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object>> include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }


        public async Task RemoveAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
           _context.Entry(entity).State=EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
