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

namespace IsikUn.IncubationCentre.Currencies
{
    public class EFCoreCurrencyRepository :
        EfCoreRepository<IncubationCentreDbContext, Currency, Guid>,
        ICurrencyRepository
    {
        public EFCoreCurrencyRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public async Task<List<Currency>> GetListAsync(
            string filter = null, 
            string country = null, 
            string title = null, 
            string symbol = null, 
            bool filterByIsDefault = false, 
            bool isDefault = false, 
            int skipCount = 0, 
            int maxResultCount = int.MaxValue, 
            string sorting = null, 
            CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                           await GetQueryableAsync(),
                           filter,
                           country, title, symbol,filterByIsDefault,isDefault
                           );
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "Name asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        public async Task<long> GetCountAsync(string filter = null, string country = null, string title = null, string symbol = null, bool filterByIsDefault = false, bool isDefault = false, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                        await GetQueryableAsync(),
                        filter,
                        country, title, symbol, filterByIsDefault, isDefault
                        );
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        protected virtual IQueryable<Currency> ApplyFilter(
            IQueryable<Currency> query,
            string filter = null,
            string country = null,
            string title = null,
            string symbol = null,
            bool filterByIsDefault = false,
            bool isDefault = false
            )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e => e.Country.Contains(filter) || e.Title.Contains(filter) || e.Symbol.Contains(filter))
                    .WhereIf(!string.IsNullOrWhiteSpace(country), e => e.Country.Contains(country))
                    .WhereIf(!string.IsNullOrWhiteSpace(symbol), e => e.Symbol.Contains(symbol))
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(filterByIsDefault, e => e.IsDefault == isDefault);
        }
    }
}
