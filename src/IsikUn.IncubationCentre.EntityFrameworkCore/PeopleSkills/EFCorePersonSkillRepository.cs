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

namespace IsikUn.IncubationCentre.PeopleSkills
{
    public class EFCorePersonSkillRepository :
        EfCoreRepository<IncubationCentreDbContext, PersonSkill, Guid>,
        IPersonSkillRepository
    {
        public EFCorePersonSkillRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(Guid[] userIds = null, Guid[] skillIds = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        (await GetQueryableAsync())
                        ,
                        userIds,
                        skillIds
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<PersonSkill>> GetListAsync(Guid[] userIds = null, Guid[] skillIds = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                    (await WithDetailsAsync(c => c.Skill, d => d.Person))
                                    ,
                                    userIds,
                                    skillIds
                                    );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? " " : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<PersonSkill> ApplyFilter(
             IQueryable<PersonSkill> query,
             Guid[] userIds = null,
             Guid[] SkillIds = null
        )
        {
            return query
                    .WhereIf(SkillIds != null && SkillIds.Any(), e => SkillIds.Contains(e.SkillId))
                    .WhereIf(userIds != null && userIds.Any(), e => userIds.Contains(e.PersonId));
        }
    }
}


