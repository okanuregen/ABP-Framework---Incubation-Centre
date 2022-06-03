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

namespace IsikUn.IncubationCentre.Events
{
    public class EventAppService : ApplicationService, IEventAppService
    {
        private readonly IEventRepository _eventRepository;

        public EventAppService(IEventRepository eventRepository)
        {
            this._eventRepository = eventRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Events.Create)]
        public async Task<EventDto> CreateAsync(CreateUpdateEventDto input)
        {
            var projectHasTheEvent = await _eventRepository.FindAsync(a => a.ProjectId == input.ProjectId && a.Title == input.Title);
            if (projectHasTheEvent != null)
            {
                throw new UserFriendlyException(L["ProjectHasTheEvent"]);
            }

            var Event = ObjectMapper.Map<CreateUpdateEventDto, Event>(input);
            Event = await _eventRepository.InsertAsync(Event, autoSave: true);
            return ObjectMapper.Map<Event, EventDto>(Event);
        }

        [Authorize(IncubationCentrePermissions.Events.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _eventRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Events.Default)]
        public async Task<List<EventDto>> GetAllItemsAsync()
        {
            var items = (await _eventRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Event>, List<EventDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Events.Default)]
        public async Task<EventDto> GetAsync(Guid id)
        {
            var Event = await _eventRepository.GetAsync(id);
            return ObjectMapper.Map<Event, EventDto>(Event);
        }

        [Authorize(IncubationCentrePermissions.Events.Default)]
        public async Task<PagedResultDto<EventDto>> GetListAsync(GetEventsInput input)
        {
            var totalCount = await _eventRepository.GetCountAsync(input.filter,input.Title,input.Description,input.ProjectName,input.CreatorUserName);
            var items = await _eventRepository.GetListAsync(input.filter, input.Title, input.Description, input.ProjectName, input.CreatorUserName, input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<EventDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Event>, List<EventDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Events.Edit)]
        public async Task<EventDto> UpdateAsync(Guid id, CreateUpdateEventDto input)
        {
            var ProjectHasTheEvent = await _eventRepository.FindAsync(a => a.ProjectId == input.ProjectId && a.Title == input.Title && a.Id != id);
            if (ProjectHasTheEvent != null)
            {
                throw new UserFriendlyException(L["ProjectHasTheEvent"]);
            }

            var Event = await _eventRepository.GetAsync(id);
            ObjectMapper.Map(input, Event);
            Event = await _eventRepository.UpdateAsync(Event, autoSave: true);
            return ObjectMapper.Map<Event, EventDto>(Event);
        }
    }
}
