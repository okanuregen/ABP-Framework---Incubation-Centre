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

namespace IsikUn.IncubationCentre.Skills
{
    public class EFCoreSkillRepository :
        EfCoreRepository<IncubationCentreDbContext, Skill, Guid>,
        ISkillRepository
    {
        public EFCoreSkillRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Skill>> GetListAsync(
             string filter = null,
             string name = null,
             string category = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
            )
        {
            var query = ApplyFilter(
                (await WithDetailsAsync(a => a.People))
                , filter, name, category);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "Name asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }
        public async Task<long> GetCountAsync(
             string filter = null,
             string name = null,
             string category = null,
             CancellationToken cancelationToken = default
            )
        {
            var query = ApplyFilter(
                (await WithDetailsAsync(a => a.People))
                , filter, name, category);
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));

        }

        protected virtual IQueryable<Skill> ApplyFilter(
            IQueryable<Skill> query,
            string filter,
            string name = null,
            string category = null)
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.Contains(filter) || e.Category.Contains(filter))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name))
                    .WhereIf(!string.IsNullOrWhiteSpace(category), e => e.Category.Contains(category));
        }
    }
}
