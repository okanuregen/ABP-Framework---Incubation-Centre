using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Currencies
{
    public interface ICurrencyRepository : IRepository<Currency, Guid>
    {
        Task<List<Currency>> GetListAsync(
             string filter = null,
             string country = null,
             string title = null,
             string symbol = null,
             bool filterByIsDefault = false,
             bool isDefault = false,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             string filter = null,
             string country = null,
             string title = null,
             string symbol = null,
             bool filterByIsDefault = false,
             bool isDefault = false,
             CancellationToken cancelationToken = default
            );
    }
}