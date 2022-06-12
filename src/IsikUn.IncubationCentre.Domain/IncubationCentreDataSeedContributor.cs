using IsikUn.IncubationCentre.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;

namespace IsikUn.IncubationCentre.DbMigrator
{
    public class IncubationCentreDataSeedContributor
        : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Currency, Guid> _currencyRepository;
        private readonly ICurrentTenant _currentTenant;

        public IncubationCentreDataSeedContributor(
            IRepository<Currency, Guid> currencyRepository,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant)
        {
            _currencyRepository = currencyRepository;
            _currentTenant = currentTenant;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            using (_currentTenant.Change(context?.TenantId))
            {
                if (await _currencyRepository.GetCountAsync() > 0)
                {
                    return;
                }

                var currency = new Currency
                {
                    Country = "Turkey",
                    Title = "Turkish Lira",
                    Symbol = "₺",
                    IsDefault = true,
                };
                var currency2 = new Currency
                {
                    Country = "ABD",
                    Title = "American Dollar",
                    Symbol = "$",
                    IsDefault = false,
                };

                await _currencyRepository.InsertAsync(currency);
                await _currencyRepository.InsertAsync(currency2);
            }
        }
    }
}
