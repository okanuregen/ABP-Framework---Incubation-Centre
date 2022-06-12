using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Events
{
    public interface IEventRepository : IRepository<Event, Guid>
    {
        Task<List<Event>> GetListAsync(
             string filter = null,
             string title = null,
             string description = null,
             string projectName = null,
             string creatorUserName = null,
             Guid[] projectIds = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
            string filter = null,
             string title = null,
             string description = null,
             string projectName = null,
             string creatorUserName = null,
             Guid[] projectIds = null,
             CancellationToken cancelationToken = default
            );

    }
}
