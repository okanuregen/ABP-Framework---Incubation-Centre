using System;
using IsikUn.IncubationCentre.Requests.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Requests
{
    public interface IRequestAppService :
        ICrudAppService< 
            RequestDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateRequestDto,
            UpdateRequestDto>
    {

    }
}