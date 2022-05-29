using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.ProjectsInvestors
{
    public class ProjectInvestor : AuditedEntity<Guid>
    {
        public Guid ProjectId { get; set; }
        public Guid InvestorId { get; set; }
        public Project Project { get; set; }
        public Investor Investor { get; set; }
    }
}
