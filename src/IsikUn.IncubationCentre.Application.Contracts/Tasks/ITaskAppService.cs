using System;
using IsikUn.IncubationCentre.Tasks.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Tasks
{
    public interface ITaskAppService :
        ICrudAppService< 
            TaskDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateTaskDto,
            UpdateTaskDto>
    {

    }
}