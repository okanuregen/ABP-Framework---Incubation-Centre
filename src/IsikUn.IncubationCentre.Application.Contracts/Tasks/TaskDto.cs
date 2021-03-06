using IsikUn.IncubationCentre.People;
using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Tasks
{
    [Serializable]
    public class TaskDto : FullAuditedEntityDto<Guid>
    {
        public Guid AssignedToId { get; set; }

        public PersonDto AssignedTo { get; set; }

        public bool isDone { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? CompletionDate { get; set; }
    }
}