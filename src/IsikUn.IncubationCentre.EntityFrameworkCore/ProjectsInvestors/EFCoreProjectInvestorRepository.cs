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

namespace IsikUn.IncubationCentre.ProjectsInvestors
{
    public class EFCoreProjectInvestorRepository :
        EfCoreRepository<IncubationCentreDbContext, ProjectInvestor, Guid>,
        IProjectInvestorRepository
    {
        public EFCoreProjectInvestorRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(Guid[] InvestorIds = null, Guid[] ProjectIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        InvestorIds,
                        ProjectIds
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectInvestor>> GetListAsync(Guid[] InvestorIds = null, Guid[] ProjectIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(c => c.Investor, d => d.Project))
                                    ,
                                    InvestorIds,
                                    ProjectIds
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectInvestor> ApplyFilter(
             IQueryable<ProjectInvestor> query,
             Guid[] InvestorIds = null,
             Guid[] ProjectIds = null
        )
        {
            return query
                    .WhereIf(InvestorIds != null && InvestorIds.Any(), e => InvestorIds.Contains(e.InvestorId))
                    .WhereIf(ProjectIds != null && ProjectIds.Any(), e => ProjectIds.Contains(e.ProjectId));
        }
    }
}


