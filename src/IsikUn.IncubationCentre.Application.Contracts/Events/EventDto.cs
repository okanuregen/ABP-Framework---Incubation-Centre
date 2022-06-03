using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Events
{
    [Serializable]
    public class EventDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public Guid? ProjectId { get; set; }

        public ProjectDto Project { get; set; }
        public PersonDto Creator { get; set; }

    }
}