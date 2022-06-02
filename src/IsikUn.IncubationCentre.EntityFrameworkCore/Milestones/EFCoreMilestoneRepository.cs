using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using IsikUn.IncubationCentre.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Milestones
{
    public class EFCoreMilestoneRepository : EfCoreRepository<IncubationCentreDbContext, Milestone, Guid>,
        IMilestoneRepository
    {
        public EFCoreMilestoneRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Milestone>> GetListAsync(
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
            )
        {
            var query = ApplyFilter(
                (await GetQueryableAsync())
                , filter, title,successcriteria,filterByisCompleted,isCompleted,projectId);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "Title asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }
        public async Task<long> GetCountAsync(
            string filter = null,
             string title = null,
             string successcriteria = null,
             bool filterByisCompleted = false,
             bool isCompleted = false,
             string projectId = null,
             CancellationToken cancelationToken = default
            )
        {
            var query = ApplyFilter(
                (await GetQueryableAsync())
                , filter, title, successcriteria, filterByisCompleted, isCompleted, projectId);
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));

        }

        protected virtual IQueryable<Milestone> ApplyFilter(
            IQueryable<Milestone> query,
            string filter = null,
             string title = null,
             string successcriteria = null,
             bool filterByisCompleted = false,
             bool isCompleted = false,
             string projectId = null
            )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter),
                            e => e.Title.Contains(filter) || e.SuccessCriteria.Contains(filter) || e.ProjectId.ToString() == filter || e.Id.ToString() == filter)
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(!string.IsNullOrWhiteSpace(successcriteria), e => e.SuccessCriteria.Contains(successcriteria))
                    .WhereIf(!string.IsNullOrWhiteSpace(projectId), e => e.ProjectId.ToString() == projectId)
                    .WhereIf(filterByisCompleted, e => e.isCompleted == isCompleted);
        }
    }
}
