using System;
using System.Linq;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Tasks
{
    public class TaskRepository : EfCoreRepository<IncubationCentreDbContext, Task, Guid>, ITaskRepository
    {
        public TaskRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Task>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
