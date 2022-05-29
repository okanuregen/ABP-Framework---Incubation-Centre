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

namespace IsikUn.IncubationCentre.ProjectsCollaborators
{
    public class EFCoreProjectCollaboratorRepository :
        EfCoreRepository<IncubationCentreDbContext, ProjectCollaborator, Guid>,
        IProjectCollaboratorRepository
    {
        public EFCoreProjectCollaboratorRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(Guid[] CollaboratorIds = null, Guid[] ProjectIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        CollaboratorIds,
                        ProjectIds
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectCollaborator>> GetListAsync(Guid[] CollaboratorIds = null, Guid[] ProjectIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(c => c.Collaborator, d => d.Project))
                                    ,
                                    CollaboratorIds,
                                    ProjectIds
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectCollaborator> ApplyFilter(
             IQueryable<ProjectCollaborator> query,
             Guid[] CollaboratorIds = null,
             Guid[] ProjectIds = null
        )
        {
            return query
                    .WhereIf(CollaboratorIds != null && CollaboratorIds.Any(), e => CollaboratorIds.Contains(e.CollaboratorId))
                    .WhereIf(ProjectIds != null && ProjectIds.Any(), e => ProjectIds.Contains(e.ProjectId));
        }
    }
}


