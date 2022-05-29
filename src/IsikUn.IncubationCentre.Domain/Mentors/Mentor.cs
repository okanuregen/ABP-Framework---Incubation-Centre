using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.ProjectsMentors;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Mentors
{
    public class Mentor : Person
    {
        public virtual bool isActivated { get; set; }

        public ICollection<Project> MentoringProjects { get; set; }
        public List<ProjectMentor> ProjectsMentors { get; set; }
    }
}
