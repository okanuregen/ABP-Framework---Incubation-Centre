using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
namespace IsikUn.IncubationCentre.Milestones
{
    public interface IMilestoneRepository : IRepository<Milestone, Guid>
    {
        Task<List<Milestone>> GetListAsync(
             string filter = null,
             string title = null,
             string successcriteria = null,
             bool filterByisCompleted = false,
             bool isCompleted = false,
             string projectId = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             string filter = null,
             string title = null,
             string successcriteria = null,
             bool filterByisCompleted = false,
             bool isCompleted = false,
             string projectId = null,
             CancellationToken cancelationToken = default
            );
        
    }
}
