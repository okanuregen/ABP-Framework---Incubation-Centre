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

namespace IsikUn.IncubationCentre.ProjectsEntrepreneurs
{
    public class EFCoreProjectEntrepreneurRepository :
        EfCoreRepository<IncubationCentreDbContext, ProjectEntrepreneur, Guid>,
        IProjectEntrepreneurRepository
    {
        public EFCoreProjectEntrepreneurRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(Guid[] EntrepreneurIds = null, Guid[] ProjectIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        EntrepreneurIds,
                        ProjectIds
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectEntrepreneur>> GetListAsync(Guid[] EntrepreneurIds = null, Guid[] ProjectIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(c => c.Entrepreneur, d => d.Project))
                                    ,
                                    EntrepreneurIds,
                                    ProjectIds
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectEntrepreneur> ApplyFilter(
             IQueryable<ProjectEntrepreneur> query,
             Guid[] EntrepreneurIds = null,
             Guid[] ProjectIds = null
        )
        {
            return query
                    .WhereIf(EntrepreneurIds != null && EntrepreneurIds.Any(), e => EntrepreneurIds.Contains(e.EntrepreneurId))
                    .WhereIf(ProjectIds != null && ProjectIds.Any(), e => ProjectIds.Contains(e.ProjectId));
        }
    }
}


