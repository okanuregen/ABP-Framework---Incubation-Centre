using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Events
{
    public class Event : FullAuditedEntity<Guid>
    {
        public string Title { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public Guid ProjectId { get; set; }
        public Project Project { get; set; }
        public Person Creator { get; set; }

    }
}
