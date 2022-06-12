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

        public async Task<long> GetCountAsync(Guid? InvestorId = null, Guid? ProjectId = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        InvestorId,
                        ProjectId
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectInvestor>> GetListAsync(Guid? InvestorId = null, Guid? ProjectId = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await GetQueryableAsync()).Include(a => a.Project).Include(a => a.Investor).ThenInclude(b => b.IdentityUser)
                                    ,
                                    InvestorId,
                                    ProjectId
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectInvestor> ApplyFilter(
             IQueryable<ProjectInvestor> query,
             Guid? InvestorId = null,
             Guid? ProjectId = null
        )
        {
            return query
                    .WhereIf(InvestorId.HasValue, e => InvestorId.Value == e.InvestorId)
                    .WhereIf(ProjectId.HasValue, e => ProjectId.Value == e.ProjectId);
        }

        public async Task<List<ProjectInvestor>> GetAllWithDetailAsync()
        {
            return (await GetDbSetAsync())
              .Include(c => c.Investor).ThenInclude(a => a.IdentityUser)
              .Include(c => c.Project).ToList();
        }
    }
}


