using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Events
{
    public interface IEventAppService : IApplicationService
    {
        Task<PagedResultDto<EventDto>> GetListAsync(GetEventsInput input);

        Task<List<EventDto>> GetAllItemsAsync();

        Task<EventDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<EventDto> CreateAsync(CreateUpdateEventDto input);

        Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input);
    }
}

