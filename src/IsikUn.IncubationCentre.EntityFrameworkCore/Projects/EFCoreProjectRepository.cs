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
using Volo.Abp.Identity;
using IsikUn.IncubationCentre.Projects;

namespace IsikUn.IncubationCentre.Mentors
{
    public class EFCoreProjectRepository :
        EfCoreRepository<IncubationCentreDbContext, Project, Guid>,
        IProjectRepository
    {
        public EFCoreProjectRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Project>> GetListAsync(ProjectStatus status, bool filterByStatus = false, string filter = null, string name = null, string tags = null, bool filterByinvesmentReady = false, bool invesmentReady = false, bool filterByopenForInvesment = false, bool openForInvesment = false, List<Guid> founderIds = null, List<Guid> investorIds = null, List<Guid> mentorIds = null, List<Guid> collaboratorIds = null, Guid[] entrepreneurIds = null, bool filterByNoMentor = false,
           bool NoMentor = false, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                          (await GetQueryableAsync())
                          .Include(a => a.Mentors).ThenInclude(b => b.IdentityUser)
                          .Include(a => a.Founders).ThenInclude(b => b.IdentityUser)
                          .Include(a => a.Investors).ThenInclude(b => b.IdentityUser)
                          .Include(a => a.Collaborators).ThenInclude(b => b.IdentityUser)
                          .Include(a => a.Entrepreneurs).ThenInclude(b => b.IdentityUser),
           status,
           filterByStatus,
           filter,
           name,
           tags,
           filterByinvesmentReady,
           invesmentReady,
           filterByopenForInvesment,
           openForInvesment,
           founderIds,
           investorIds,
           mentorIds,
           collaboratorIds,
           entrepreneurIds,
           filterByNoMentor,
           NoMentor
                          );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? " Name asc " : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        public async Task<long> GetCountAsync(ProjectStatus status, bool filterByStatus = false, string filter = null, string name = null, string tags = null, bool filterByinvesmentReady = false, bool invesmentReady = false, bool filterByopenForInvesment = false, bool openForInvesment = false, List<Guid> founderIds = null, List<Guid> investorIds = null, List<Guid> mentorIds = null, List<Guid> collaboratorIds = null, Guid[] entrepreneurIds = null, bool filterByNoMentor = false,
           bool NoMentor = false, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                          (await GetQueryableAsync())
                          .Include(a => a.Mentors)
                          .Include(a => a.Founders)
                          .Include(a => a.Investors)
                          .Include(a => a.Collaborators)
                          .Include(a => a.Entrepreneurs),
                       status,
                       filterByStatus,
                       filter,
                       name,
                       tags,
                       filterByinvesmentReady,
                       invesmentReady,
                       filterByopenForInvesment,
                       openForInvesment,
                       founderIds,
                       investorIds,
                       mentorIds,
                       collaboratorIds,
                       entrepreneurIds,
                       filterByNoMentor,
                       NoMentor
                          );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        protected virtual IQueryable<Project> ApplyFilter(
          IQueryable<Project> query,
           ProjectStatus status,
           bool filterByStatus = false,
           string filter = null,
           string name = null,
           string tags = null,
           bool filterByinvesmentReady = false,
           bool invesmentReady = false,
           bool filterByopenForInvesment = false,
           bool openForInvesment = false,
           List<Guid> founderIds = null,
           List<Guid> investorIds = null,
           List<Guid> mentorIds = null,
           List<Guid> collaboratorIds = null,
           Guid[] entrepreneurIds = null,
           bool filterByNoMentor = false,
           bool NoMentor = false
       )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e =>
                        (e.Name != null ? e.Name.Contains(filter) : false) ||
                        (e.Tags != null ? e.Tags.Contains(filter) : false) ||
                        (e.Id.ToString().Contains(filter))
                        )
                    .WhereIf(filterByStatus, e => e.Status == status)
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(tags), e => e.Name.Contains(tags))
                    .WhereIf(filterByinvesmentReady, e => e.InvesmentReady == invesmentReady)
                    .WhereIf(filterByopenForInvesment, e => e.OpenForInvesment == openForInvesment)
                    .WhereIf(filterByNoMentor, e => NoMentor ? (e.Mentors == null || e.Mentors.Count() == 0) : (e.Mentors != null && e.Mentors.Count() > 0))
                    .WhereIf(founderIds != null && founderIds.Any(), e => founderIds.All(a => e.Founders.Select(b => b.Id).Contains(a)))
                    .WhereIf(investorIds != null && investorIds.Any(), e => investorIds.All(a => e.Investors.Select(b => b.Id).Contains(a)))
                    .WhereIf(mentorIds != null && mentorIds.Any(), e => mentorIds.All(a => e.Mentors.Select(b => b.Id).Contains(a)))
                    .WhereIf(entrepreneurIds != null && entrepreneurIds.Any(), e => entrepreneurIds.All(a => e.Entrepreneurs.Select(b => b.Id).Contains(a)))
                    .WhereIf(collaboratorIds != null && collaboratorIds.Any(), e => collaboratorIds.All(a => e.Collaborators.Select(b => b.Id).Contains(a)));
        }

        public async Task<Project> GetWithDetailAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dbSet = (await GetDbSetAsync())
                .Where(a => a.Id == id)
                .Include(c => c.Entrepreneurs).ThenInclude(a => a.IdentityUser)
                .Include(c => c.Collaborators).ThenInclude(a => a.IdentityUser)
                .Include(c => c.Investors).ThenInclude(a => a.IdentityUser)
                .Include(c => c.Mentors).ThenInclude(a => a.IdentityUser)
                .Include(c => c.Milestones)
                .Include(a => a.Founders).ThenInclude(a => a.IdentityUser);
            var rs = await dbSet.FirstOrDefaultAsync(cancellationToken);
            return rs;
        }
    }
}

