using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Investors
{
    public interface IInvestorAppService : IApplicationService
    {
        Task<PagedResultDto<InvestorDto>> GetListAsync(GetInvestorsInput input);

        Task<List<InvestorDto>> GetAllItemsAsync();

        Task<InvestorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<InvestorDto> CreateAsync(CreateUpdateInvestorDto input);

        Task<InvestorDto> UpdateAsync(Guid id, CreateUpdateInvestorDto input);
    }
}
