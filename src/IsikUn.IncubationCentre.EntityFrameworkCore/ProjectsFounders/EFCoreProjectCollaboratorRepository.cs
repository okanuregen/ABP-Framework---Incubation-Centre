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
using IsikUn.IncubationCentre.Skills;

namespace IsikUn.IncubationCentre.ProjectsFounders
{
    public class EFCoreProjectFounderRepository :
        EfCoreRepository<IncubationCentreDbContext, ProjectFounder, Guid>,
        IProjectFounderRepository
    {
        public EFCoreProjectFounderRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(Guid[] FounderIds = null, Guid[] ProjectIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        FounderIds,
                        ProjectIds
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectFounder>> GetListAsync(Guid[] FounderIds = null, Guid[] ProjectIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(c => c.Person, d => d.Project))
                                    ,
                                    FounderIds,
                                    ProjectIds
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectFounder> ApplyFilter(
             IQueryable<ProjectFounder> query,
             Guid[] FounderIds = null,
             Guid[] ProjectIds = null
        )
        {
            return query
                    .WhereIf(FounderIds != null && FounderIds.Any(), e => FounderIds.Contains(e.PersonId))
                    .WhereIf(ProjectIds != null && ProjectIds.Any(), e => ProjectIds.Contains(e.ProjectId));
        }
    }
}


