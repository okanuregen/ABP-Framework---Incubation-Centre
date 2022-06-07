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

namespace IsikUn.IncubationCentre.People
{
    public class EFCorePersonRepository :
        EfCoreRepository<IncubationCentreDbContext, Person, Guid>,
        IPersonRepository
    {
        public EFCorePersonRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string filter = null, string userName = null, string name = null, string surname = null, string email = null, string phoneNumber = null, string experience = null, Guid[] skillIds = null, string about = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await WithDetailsAsync(a => a.IdentityUser, b => b.Skills))
                        ,
                        filter,
                        userName,
                        name,
                        surname,
                        email,
                        phoneNumber,
                        experience,
                        skillIds,
                        about
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<Person>> GetListAsync(string filter = null, string userName = null, string name = null, string surname = null, string email = null, string phoneNumber = null, string experience = null, Guid[] skillIds = null, string about = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(a => a.IdentityUser, b => b.Skills))
                                    ,
                                    filter,
                                    userName,
                                    name,
                                    surname,
                                    email,
                                    phoneNumber,
                                    experience,
                                    skillIds,
                                    about
                                    );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? " " : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        public async Task<Person> GetWithDetailByIdentityUserIdAsync(Guid IdentityUserId, CancellationToken cancelationToken = default)
        {
            var dbSet = (await GetDbSetAsync()).Where(a => a.IdentityUserId == IdentityUserId)
               .Include(c => c.IdentityUser)
               .Include(a => a.SentRequests)
               .Include(a => a.ReceivedRequests)
               .Include(a => a.Skills)
               .Include(a => a.Tasks)
               .Include(a => a.FoundedProjects);
            var rs = await dbSet.FirstOrDefaultAsync(cancelationToken);
            return rs;
        }

        protected virtual IQueryable<Person> ApplyFilter(
           IQueryable<Person> query,
             string filter = null,
             string userName = null,
             string name = null,
             string surname = null,
             string email = null,
             string phoneNumber = null,
             string experience = null,
             Guid[] SkillIds = null,
             string about = null
        )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e =>
                        (e.IdentityUser != null ? e.IdentityUser.UserName.Contains(filter) : false) ||
                        (e.IdentityUser != null ? e.IdentityUser.Name.Contains(filter) : false) ||
                        (e.IdentityUser != null ? e.IdentityUser.Surname.Contains(filter) : false) ||
                        (e.IdentityUser != null ? e.IdentityUser.Email.Contains(filter) : false) ||
                        e.Experience.Contains(filter) ||
                        e.About.Contains(filter)
                        )
                    .WhereIf(!string.IsNullOrWhiteSpace(userName), e => (e.IdentityUser != null ? e.IdentityUser.UserName.Contains(userName) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => (e.IdentityUser != null ? e.IdentityUser.Name.Contains(name) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(surname), e => (e.IdentityUser != null ? e.IdentityUser.Surname.Contains(surname) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(email), e => (e.IdentityUser != null ? e.IdentityUser.Email.Contains(email) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => (e.IdentityUser != null ? e.IdentityUser.PhoneNumber.Contains(phoneNumber) : false))
                    .WhereIf(!string.IsNullOrWhiteSpace(about), e => e.About.Contains(about))
                    .WhereIf(SkillIds != null && SkillIds.Any(), e => SkillIds.All(a => e.Skills.Select(x => x.Id).Contains(a)));
        }
    }
}


