using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Events
{
    public static class EventEfCoreQueryableExtensions
    {
        public static IQueryable<Event> IncludeDetails(this IQueryable<Event> queryable, bool include = true)
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