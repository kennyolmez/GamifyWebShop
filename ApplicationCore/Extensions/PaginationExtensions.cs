using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Extensions
{
    public static class PaginationExtensions
    {
        // IQueryable instead of IEnumerable here because we're querying out of memory data (_context.Products). This means we dont want to materialize 
        // the query until all the expressions of the chain method are read
        public static IQueryable<T> Paginate<T>(this IQueryable<T> query, int page, int perPage)
        {
            if (page < 1)
                throw new ArgumentOutOfRangeException(nameof(page), "Page has to be greater than 0");
            if (perPage < 1)
                throw new ArgumentOutOfRangeException(nameof(perPage), "PerPage has to be greater than 0");

            return query
                .Skip(Math.Max(0, page - 1) * perPage)
                .Take(perPage);
        }
    }
}
