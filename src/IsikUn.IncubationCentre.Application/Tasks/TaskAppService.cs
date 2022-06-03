using System;
using IsikUn.IncubationCentre.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp;
using System.Collections.Generic;
using System.Linq;
using IsikUn.IncubationCentre.Localization;

namespace IsikUn.IncubationCentre.Tasks
{
    public class TaskAppService : ApplicationService, ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskAppService(ITaskRepository taskRepository)
        {
            this._taskRepository = taskRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Tasks.Create)]
        public async Task<TaskDto> CreateAsync(CreateUpdateTaskDto input)
        {
            var Task = ObjectMapper.Map<CreateUpdateTaskDto, Task>(input);
            Task = await _taskRepository.InsertAsync(Task, autoSave: true);
            return ObjectMapper.Map<Task, TaskDto>(Task);
        }

        [Authorize(IncubationCentrePermissions.Tasks.Delete)]
        public async System.Threading.Tasks.Task DeleteAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Tasks.Default)]
        public async Task<List<TaskDto>> GetAllItemsAsync()
        {
            var items = (await _taskRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Task>, List<TaskDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Tasks.Default)]
        public async Task<TaskDto> GetAsync(Guid id)
        {
            var Task = await _taskRepository.GetAsync(id);
            return ObjectMapper.Map<Task, TaskDto>(Task);
        }

        [Authorize(IncubationCentrePermissions.Tasks.Default)]
        public async Task<PagedResultDto<TaskDto>> GetListAsync(GetTasksInput input)
        {
            var totalCount = await _taskRepository.GetCountAsync(input.filter, input.Title, input.Description,input.AssignedUserName);
            var items = await _taskRepository.GetListAsync(input.filter, input.Title, input.Description, input.AssignedUserName, input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<TaskDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Task>, List<TaskDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Tasks.Edit)]
        public async Task<TaskDto> UpdateAsync(Guid id, CreateUpdateTaskDto input)
        {
            var Task = await _taskRepository.GetAsync(id);
            ObjectMapper.Map(input, Task);
            Task = await _taskRepository.UpdateAsync(Task, autoSave: true);
            return ObjectMapper.Map<Task, TaskDto>(Task);
        }
    }
}
