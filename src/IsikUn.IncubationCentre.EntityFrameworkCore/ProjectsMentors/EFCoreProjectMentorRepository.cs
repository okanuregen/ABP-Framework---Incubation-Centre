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

        public async Task<long> GetCountAsync(Guid? MentorId = null, Guid? ProjectId = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        MentorId,
                        ProjectId
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<ProjectMentor>> GetListAsync(Guid? MentorId = null, Guid? ProjectId = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await GetQueryableAsync()).Include(a => a.Project).Include(a => a.Mentor).ThenInclude(b => b.IdentityUser)
                                    ,
                                    MentorId,
                                    ProjectId
                                    );
            query = string.IsNullOrWhiteSpace(sorting) ? query : query.OrderBy(sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<ProjectMentor> ApplyFilter(
             IQueryable<ProjectMentor> query,
             Guid? MentorId = null,
             Guid? ProjectId = null
        )
        {
            return query
                    .WhereIf(MentorId.HasValue, e => MentorId.Value == e.MentorId)
                    .WhereIf(ProjectId.HasValue, e => ProjectId.Value == e.ProjectId);
        }

        public async Task<List<ProjectMentor>> GetAllWithDetailAsync()
        {
            return (await GetDbSetAsync())
              .Include(c => c.Mentor).ThenInclude(a => a.IdentityUser)
              .Include(c => c.Project).ToList();
        }
    }
}


