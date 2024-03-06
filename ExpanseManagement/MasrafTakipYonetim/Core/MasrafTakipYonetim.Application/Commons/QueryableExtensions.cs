using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Commons
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> WhereIf<TEntity>(this IQueryable<TEntity> source, bool condition, Expression<Func<TEntity, bool>> predicate)
        {
            return condition ? source.Where(predicate) : source;
        }

        public static PaginatedList<TDto> QueryResource<TEntity, TDto>(this IQueryable<TEntity> resource,
                                                                            IMapper mapper,
                                                                            int pageNumber = 1,
                                                                            int pageSize = 10)
                                                                                          where TEntity : class
                                                                                          where TDto : class
        {
            IQueryable<TEntity> query = resource.AsNoTracking();
            return query.ProjectTo<TDto>(mapper.ConfigurationProvider)
                        .MappedPaginatedList(pageNumber, pageSize);
        }

        public static PaginatedList<TDto> QueryResourceNotMapped<TDto>(this IQueryable<TDto> resource,

                                                                            int pageNumber = 1,
                                                                            int pageSize = 10)

                                                                                          where TDto : class
        {
            IQueryable<TDto> query = resource.AsNoTracking();
            return query
                        .MappedPaginatedList(pageNumber, pageSize);
        }

    }
}
