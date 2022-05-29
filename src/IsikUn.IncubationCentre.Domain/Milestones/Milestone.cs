using IsikUn.IncubationCentre.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Milestones
{
    public class Milestone : FullAuditedEntity<Guid> 
    {
        public string Title { get; set; }
        public string SuccessCriteria { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isCompleted { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
