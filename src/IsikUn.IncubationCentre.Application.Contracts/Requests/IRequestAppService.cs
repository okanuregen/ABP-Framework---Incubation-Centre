using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Requests
{
    public interface IRequestAppService : IApplicationService
    {
        Task<PagedResultDto<RequestDto>> GetListAsync(GetRequestsInput input);

        Task<List<RequestDto>> GetAllItemsAsync();

        Task<RequestDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<RequestDto> CreateAsync(CreateUpdateRequestDto input);

        Task<RequestDto> UpdateAsync(Guid id, CreateUpdateRequestDto input);
    }
}
