using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasrafTakipYonetim.Application.Commons
{
    public static class MappingExtensions
    {
        public static PaginatedList<TDestination> MappedPaginatedList<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize)
        {
            return PaginatedList<TDestination>.Create(queryable, pageNumber, pageSize);
        }
    }
}
