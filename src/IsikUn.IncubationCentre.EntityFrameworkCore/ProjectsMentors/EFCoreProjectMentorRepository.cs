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

namespace IsikUn.IncubationCentre.ProjectsMentors
{
    public class EFCoreProjectMentorRepository :
        EfCoreRepository<IncubationCentreDbContext, ProjectMentor, Guid>,
        IProjectMentorRepository
    {
        public EFCoreProjectMentorRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(Guid[] MentorIds = null, Guid[] ProjectIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        MentorIds,
                        ProjectIds
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectMentor>> GetListAsync(Guid[] MentorIds = null, Guid[] ProjectIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(c => c.Mentor, d => d.Project))
                                    ,
                                    MentorIds,
                                    ProjectIds
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectMentor> ApplyFilter(
             IQueryable<ProjectMentor> query,
             Guid[] MentorIds = null,
             Guid[] ProjectIds = null
        )
        {
            return query
                    .WhereIf(MentorIds != null && MentorIds.Any(), e => MentorIds.Contains(e.MentorId))
                    .WhereIf(ProjectIds != null && ProjectIds.Any(), e => ProjectIds.Contains(e.ProjectId));
        }
    }
}


