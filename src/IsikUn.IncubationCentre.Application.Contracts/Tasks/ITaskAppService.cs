using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Tasks
{
    public interface ITaskAppService : IApplicationService
    {
        Task<PagedResultDto<TaskDto>> GetListAsync(GetTasksInput input);

        Task<List<TaskDto>> GetAllItemsAsync();

        Task<TaskDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<TaskDto> CreateAsync(CreateUpdateTaskDto input);

        Task<TaskDto> UpdateAsync(Guid id, CreateUpdateTaskDto input);
    }
}

