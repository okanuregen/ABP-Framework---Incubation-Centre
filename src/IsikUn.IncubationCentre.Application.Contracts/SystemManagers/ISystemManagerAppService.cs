using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.SystemManagers
{
    public interface ISystemManagerAppService : IApplicationService
    {
        Task<PagedResultDto<SystemManagerDto>> GetListAsync(GetSystemManagersInput input);

        Task<List<SystemManagerDto>> GetAllItemsAsync();

        Task<SystemManagerDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SystemManagerDto> CreateAsync(CreateUpdateSystemManagerDto input);

        Task<SystemManagerDto> UpdateAsync(Guid id, CreateUpdateSystemManagerDto input);
    }
}
