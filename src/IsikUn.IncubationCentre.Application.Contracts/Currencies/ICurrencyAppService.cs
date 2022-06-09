using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Currencies
{
    public interface ICurrencyAppService : IApplicationService
    {
        Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input);

        Task<List<CurrencyDto>> GetAllItemsAsync();

        Task<CurrencyDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CurrencyDto> CreateAsync(CreateUpdateCurrencyDto input);

        Task<CurrencyDto> UpdateAsync(Guid id, CreateUpdateCurrencyDto input);
    }
}
