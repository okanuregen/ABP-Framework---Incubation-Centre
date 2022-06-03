using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Requests
{
    public static class RequestEfCoreQueryableExtensions
    {
        public static IQueryable<Request> IncludeDetails(this IQueryable<Request> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                // .Include(x => x.xxx) // TODO: AbpHelper generated
                ;
        }
    }
}