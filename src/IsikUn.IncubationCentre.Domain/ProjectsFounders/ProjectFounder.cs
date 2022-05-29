using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.ProjectsFounders
{
    public class ProjectFounder : AuditedEntity<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid PersonId { get; set; }
        public Project Project { get; set; }
        public Person Person { get; set; }
    }
}
