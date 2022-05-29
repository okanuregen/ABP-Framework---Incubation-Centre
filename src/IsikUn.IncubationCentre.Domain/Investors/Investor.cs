using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.ProjectsInvestors;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Investors
{
    public class Investor : Person
    {
        public virtual bool isActivated { get; set; }

        public ICollection<Project> InvestedProjects { get; set; }
        public List<ProjectInvestor> ProjectsInvestors { get; set; }
    }
}
