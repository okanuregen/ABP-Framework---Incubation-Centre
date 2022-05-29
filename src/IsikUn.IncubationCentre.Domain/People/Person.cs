using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.ProjectsFounders;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.People
{
    public abstract class Person : FullAuditedEntity<Guid>
    {
        public string About { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public List<PersonSkill> PeopleSkills { get; set; }
        public string Experience { get; set; }
        public Guid? IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }

        public ICollection<Project> FoundedProjects { get; set; }
        public List<ProjectFounder> ProjectsFounders { get; set; }
    }
}
