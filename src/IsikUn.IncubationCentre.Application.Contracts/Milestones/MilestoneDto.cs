using IsikUn.IncubationCentre.Projects;
using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Milestones
{
    [Serializable]
    public class MilestoneDto : FullAuditedEntityDto<Guid>
    {
        public string Title { get; set; }
        public string SuccessCriteria { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isCompleted { get; set; }
        public Guid ProjectId { get; set; }
        public ProjectDto Project { get; set; }

    }
}