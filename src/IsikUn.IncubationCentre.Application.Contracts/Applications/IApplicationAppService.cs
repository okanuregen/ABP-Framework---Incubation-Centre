using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Applications
{
    public interface IApplicationAppService : IApplicationService
    {
        Task<PagedResultDto<ApplicationDto>> GetListAsync(GetApplicationsInput input);

        Task<List<ApplicationDto>> GetAllItemsAsync();

        Task<ApplicationDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ApplicationDto> CreateAsync(CreateUpdateApplicationDto input);

        Task<ApplicationDto> UpdateAsync(Guid id, CreateUpdateApplicationDto input);
      
        Task<ApplicationDto> RejectApplicationAsync(Guid id);
     
        Task<ApplicationDto> ApproveApplicationAsync(Guid id);

    }
}
