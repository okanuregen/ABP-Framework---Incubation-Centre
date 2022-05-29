using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.ProjectsCollaborators;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Collaborators
{
    public class Collaborator : Person
    {
        public virtual bool isActivated { get; set; }

        public ICollection<Project> CollaboratoringProjects { get; set; }
        public List<ProjectCollaborator> ProjectsCollaborators { get; set; }
    }
}
