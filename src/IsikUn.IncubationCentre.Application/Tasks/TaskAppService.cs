using System;
using IsikUn.IncubationCentre.Permissions;
using IsikUn.IncubationCentre.Tasks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Tasks
{
    public class TaskAppService : CrudAppService<Task, TaskDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateTaskDto, UpdateTaskDto>,
        ITaskAppService
    {
        protected override string GetPolicyName { get; set; } = IncubationCentrePermissions.Task.Default;
        protected override string GetListPolicyName { get; set; } = IncubationCentrePermissions.Task.Default;
        protected override string CreatePolicyName { get; set; } = IncubationCentrePermissions.Task.Create;
        protected override string UpdatePolicyName { get; set; } = IncubationCentrePermissions.Task.Update;
        protected override string DeletePolicyName { get; set; } = IncubationCentrePermissions.Task.Delete;

        private readonly ITaskRepository _repository;

        public TaskAppService(ITaskRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
