using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Milestones
{
    public interface IMilestoneAppService : IApplicationService
    {
        Task<PagedResultDto<MilestoneDto>> GetListAsync(GetMilestonesInput input);

        Task<List<MilestoneDto>> GetAllItemsAsync();

        Task<MilestoneDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<MilestoneDto> CreateAsync(CreateUpdateMilestoneDto input);

        Task<MilestoneDto> UpdateAsync(Guid id, CreateUpdateMilestoneDto input);
    }
}
