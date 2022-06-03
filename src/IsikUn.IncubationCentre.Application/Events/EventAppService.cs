using System;
using IsikUn.IncubationCentre.Permissions;
using IsikUn.IncubationCentre.Events.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Events
{
    public class EventAppService : CrudAppService<Event, EventDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateEventDto, UpdateEventDto>,
        IEventAppService
    {
        protected override string GetPolicyName { get; set; } = IncubationCentrePermissions.Event.Default;
        protected override string GetListPolicyName { get; set; } = IncubationCentrePermissions.Event.Default;
        protected override string CreatePolicyName { get; set; } = IncubationCentrePermissions.Event.Create;
        protected override string UpdatePolicyName { get; set; } = IncubationCentrePermissions.Event.Update;
        protected override string DeletePolicyName { get; set; } = IncubationCentrePermissions.Event.Delete;

        private readonly IEventRepository _repository;

        public EventAppService(IEventRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
