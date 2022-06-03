using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Tasks
{
    public interface ITaskRepository : IRepository<Task, Guid>
    {
        Task<List<Task>> GetListAsync(
             string filter = null,
             string name = null,
             string fullName = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             string filter = null,
             string name = null,
             string fullName = null,
             CancellationToken cancelationToken = default
            );

    }
}
