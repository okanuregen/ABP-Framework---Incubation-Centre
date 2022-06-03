using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Tasks
{
    public class TaskRepository : EfCoreRepository<IncubationCentreDbContext, Task, Guid>, 
        ITaskRepository
    {
        public TaskRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<long> GetCountAsync(string filter = null, string title = null, string description = null, string assignedToUserName = null, CancellationToken cancelationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<List<Task>> GetListAsync(string filter = null, string title = null, string description = null, string assignedToUserName = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            throw new NotImplementedException();
        }

        protected virtual IQueryable<Task> ApplyFilter(
           IQueryable<Task> query,
             string filter = null,
             string title = null,
             string description = null,
             string assignedToUserName = null
           )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter),
                            e => e.Title.Contains(filter) ||
                            e.Title.Contains(filter) ||
                            e.Description.Contains(filter) ||
                            (e.AssignedTo != null && e.AssignedTo.IdentityUser != null && e.AssignedTo.IdentityUser.UserName != null ? e.AssignedTo.IdentityUser.UserName.Contains(filter) : false)
                        )
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(!string.IsNullOrWhiteSpace(assignedToUserName), e => e.AssignedTo != null && e.AssignedTo.IdentityUser != null && e.AssignedTo.IdentityUser.UserName != null ? e.AssignedTo.IdentityUser.UserName.Contains(assignedToUserName) : false);
        }

    }
}
