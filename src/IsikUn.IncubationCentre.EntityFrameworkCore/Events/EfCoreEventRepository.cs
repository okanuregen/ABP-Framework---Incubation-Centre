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

namespace IsikUn.IncubationCentre.Events
{
    public class EFCoreEventRepository : EfCoreRepository<IncubationCentreDbContext, Event, Guid>,
        IEventRepository
    {
        public EFCoreEventRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string filter = null, string title = null, string description = null, string projectName = null, string creatorUserName = null, Guid[] projectIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                (await GetQueryableAsync())
                                    .Include(a => a.CreatorPerson)
                                    .Include(b => b.Project)
                                ,filter,title,description,projectName,creatorUserName,projectIds);
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<Event>> GetListAsync(string filter = null, string title = null, string description = null, string projectName = null, string creatorUserName = null, Guid[] projectIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                            (await GetQueryableAsync())
                                .Include(a => a.CreatorPerson)
                                .Include(b => b.Project)
                            , filter, title, description, projectName, creatorUserName,projectIds);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "Title asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<Event> ApplyFilter(
            IQueryable<Event> query,
             string filter = null,
             string title = null,
             string description = null,
             string projectName = null,
             string creatorUserName = null,
             Guid[] projectsIds = null 
            )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter),
                            e => e.Title.Contains(filter) ||
                            e.Description.Contains(filter) ||
                            (e.CreatorPerson != null && e.CreatorPerson.IdentityUser != null && e.CreatorPerson.IdentityUser.UserName != null ? e.CreatorPerson.IdentityUser.UserName.Contains(filter) : false) ||
                            (e.Project != null && e.Project.Name != null ? e.Project.Name.Contains(filter) : false)
                        )
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description))
                    .WhereIf(projectsIds != null, e => projectsIds.Contains(e.ProjectId))
                    .WhereIf(!string.IsNullOrWhiteSpace(projectName), e => e.Project != null && e.Project.Name != null ? e.Project.Name == projectName : false)
                    .WhereIf(!string.IsNullOrWhiteSpace(creatorUserName), e => e.CreatorPerson != null && e.CreatorPerson.IdentityUser != null && e.CreatorPerson.IdentityUser.UserName != null ? e.CreatorPerson.IdentityUser.UserName.Contains(creatorUserName) : false);
        }
    }
}