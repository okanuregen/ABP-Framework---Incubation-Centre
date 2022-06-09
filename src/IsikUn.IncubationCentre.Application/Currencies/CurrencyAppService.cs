using IsikUn.IncubationCentre.Localization;
using IsikUn.IncubationCentre.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Currencies
{
    [Authorize(IncubationCentrePermissions.Currencies.Default)]
    public class CurrencyAppService : ApplicationService, ICurrencyAppService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyAppService(ICurrencyRepository currencyRepository)
        {
            this._currencyRepository = currencyRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Currencies.Create)]
        public async Task<CurrencyDto> CreateAsync(CreateUpdateCurrencyDto input)
        {
            var currencyTitleExist = await _currencyRepository.AnyAsync(a => a.Title == input.Title);
            if (currencyTitleExist)
            {
                throw new UserFriendlyException(L["CurrencyTitleExist"]);
            }

            var currencySymbolExist = await _currencyRepository.AnyAsync(a => a.Symbol == input.Symbol);
            if (currencySymbolExist)
            {
                throw new UserFriendlyException(L["CurrencySymbolExist"]);
            }
            if (input.IsDefault)
            {
                var currentDefault = await _currencyRepository.GetAsync(a => a.IsDefault);
                if(currentDefault != null)
                {
                    currentDefault.IsDefault = false;
                    await _currencyRepository.UpdateAsync(currentDefault, autoSave: true);
                }
            }
            var currency = ObjectMapper.Map<CreateUpdateCurrencyDto, Currency>(input);
            currency = await _currencyRepository.InsertAsync(currency, autoSave: true);
            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        [Authorize(IncubationCentrePermissions.Currencies.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            var curreny = await _currencyRepository.GetAsync(id);
            if (curreny.IsDefault)
            {
                throw new UserFriendlyException(L["CannotDeleteDefaultCurrency"]);
            }
            await _currencyRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Currencies.Default)]
        public async Task<List<CurrencyDto>> GetAllItemsAsync()
        {
            var items = (await _currencyRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Currencies.Default)]
        public async Task<CurrencyDto> GetAsync(Guid id)
        {
            var currency = await _currencyRepository.GetAsync(id);
            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }

        [Authorize(IncubationCentrePermissions.Currencies.Default)]
        public async Task<PagedResultDto<CurrencyDto>> GetListAsync(GetCurrenciesInput input)
        {
            var totalCount = await _currencyRepository.GetCountAsync(input.filter, input.Country, input.Title, input.Symbol, input.FilterByIsDefault, input.IsDefault);
            var items = await _currencyRepository.GetListAsync(input.filter, input.Country, input.Title, input.Symbol, input.FilterByIsDefault, input.IsDefault, input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<CurrencyDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Currency>, List<CurrencyDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Currencies.Edit)]
        public async Task<CurrencyDto> UpdateAsync(Guid id, CreateUpdateCurrencyDto input)
        {
            var currencyTitleExist = await _currencyRepository.AnyAsync(a => a.Title == input.Title && a.Id != id);
            if (currencyTitleExist)
            {
                throw new UserFriendlyException(L["CurrencyTitleExist"]);
            }

            var currencySymbolExist = await _currencyRepository.AnyAsync(a => a.Symbol == input.Symbol && a.Id != id);
            if (currencySymbolExist)
            {
                throw new UserFriendlyException(L["CurrencySymbolExist"]);
            }

            if (input.IsDefault)
            {
                var currentDefault = await _currencyRepository.GetAsync(a => a.IsDefault && a.Id != id);
                if (currentDefault != null)
                {
                    currentDefault.IsDefault = false;
                    await _currencyRepository.UpdateAsync(currentDefault, autoSave: true);
                }
            }
            else
            {
                var currentDefault = await _currencyRepository.GetAsync(a => a.IsDefault);
                if(currentDefault != null && currentDefault.Id == id)
                {
                    throw new UserFriendlyException(L["MakeOtherCurrenyDefault"]);
                }
            }

            var currency = await _currencyRepository.GetAsync(id);
            ObjectMapper.Map(input, currency);
            currency = await _currencyRepository.UpdateAsync(currency, autoSave: true);
            return ObjectMapper.Map<Currency, CurrencyDto>(currency);
        }
    }
}
