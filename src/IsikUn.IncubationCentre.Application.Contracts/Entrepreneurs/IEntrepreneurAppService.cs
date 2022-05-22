using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    public interface IEntrepreneurAppService : IApplicationService
    {
        Task<PagedResultDto<EntrepreneurDto>> GetListAsync(GetEntrepreneursInput input);

        Task<List<EntrepreneurDto>> GetAllItemsAsync();

        Task<EntrepreneurDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EntrepreneurDto> CreateAsync(CreateUpdateEntrepreneurDto input);

        Task<EntrepreneurDto> UpdateAsync(Guid id, CreateUpdateEntrepreneurDto input);
    }
}
