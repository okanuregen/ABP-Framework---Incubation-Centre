using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.ProjectsMentors
{
    public class ProjectMentor : AuditedEntity<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid MentorId { get; set; }
        public Project Project { get; set; }
        public Mentor Mentor { get; set; }
    }
}
