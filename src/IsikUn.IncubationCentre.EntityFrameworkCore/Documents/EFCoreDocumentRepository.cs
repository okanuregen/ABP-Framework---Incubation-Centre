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

namespace IsikUn.IncubationCentre.Documents
{
    public class EFCoreDocumentRepository : EfCoreRepository<IncubationCentreDbContext, Document, Guid>,
        IDocumentRepository
    {
        public EFCoreDocumentRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Document>> GetListAsync(
             string filter = null,
             string name = null,
             string fullName = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
            )
        {
            var query = ApplyFilter(
                (await GetQueryableAsync())
                , filter, name);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "Name asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }
        public async Task<long> GetCountAsync(
             string filter = null,
             string name = null,
             string fullName = null,
             CancellationToken cancelationToken = default
            )
        {
            var query = ApplyFilter(
                (await GetQueryableAsync())
                , filter, name);
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));

        }

        protected virtual IQueryable<Document> ApplyFilter(
            IQueryable<Document> query,
            string filter,
            string name = null,
            string fullName = null
            )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Name.Contains(filter))
                    .WhereIf(!string.IsNullOrWhiteSpace(fullName), e => e.FullName.Contains(fullName))
                    .WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name));
        }
    }
}
