using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Tasks
{
    public static class TaskEfCoreQueryableExtensions
    {
        public static IQueryable<Task> IncludeDetails(this IQueryable<Task> queryable, bool include = true)
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