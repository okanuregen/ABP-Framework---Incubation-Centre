using System;
using IsikUn.IncubationCentre.Events.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Events
{
    public interface IEventAppService :
        ICrudAppService< 
            EventDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateEventDto,
            UpdateEventDto>
    {

    }
}