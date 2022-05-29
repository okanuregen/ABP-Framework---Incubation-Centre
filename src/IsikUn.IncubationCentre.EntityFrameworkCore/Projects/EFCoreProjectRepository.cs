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

        public async Task<List<Project>> GetListAsync(ProjectStatus status, bool filterByStatus = false, string filter = null, string name = null, string tags = null, bool filterByinvesmentReady = false, bool invesmentReady = false, bool filterByopenForInvesment = false, bool openForInvesment = false, List<Guid> founderIds = null, List<Guid> investorIds = null, List<Guid> mentorIds = null, List<Guid> collaboratorIds = null, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                          (await GetQueryableAsync()).Include(a => a.Mentors).Include(a => a.Founders).Include(a => a.Investors).Include(a => a.Collaborators),
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
           collaboratorIds
                          );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? " Name asc " : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        public async Task<long> GetCountAsync(ProjectStatus status, bool filterByStatus = false, string filter = null, string name = null, string tags = null, bool filterByinvesmentReady = false, bool invesmentReady = false, bool filterByopenForInvesment = false, bool openForInvesment = false, List<Guid> founderIds = null, List<Guid> investorIds = null, List<Guid> mentorIds = null, List<Guid> collaboratorIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                          (await GetQueryableAsync()).Include(a => a.Mentors).Include(a => a.Founders).Include(a => a.Investors).Include(a => a.Collaborators),
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
                       collaboratorIds
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
           List<Guid> collaboratorIds = null
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
                    .WhereIf(founderIds != null && founderIds.Any(), e => e.Founders.Select(a => a.Id).All(c => founderIds.Contains(c)))
                    .WhereIf(investorIds != null && investorIds.Any(), e => e.Investors.Select(a => a.Id).All(c => investorIds.Contains(c)))
                    .WhereIf(mentorIds != null && mentorIds.Any(), e => e.Mentors.Select(a => a.Id).All(c => mentorIds.Contains(c)))
                    .WhereIf(collaboratorIds != null && collaboratorIds.Any(), e => e.Collaborators.Select(a => a.Id).All(c => collaboratorIds.Contains(c)));
        }
    }
}

