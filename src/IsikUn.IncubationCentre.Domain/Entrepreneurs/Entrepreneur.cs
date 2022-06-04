using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.ProjectsEntrepreneurs;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    public class Entrepreneur : Person
    {
        public virtual bool isActivated { get; set; }

        public ICollection<Project> MyProjects { get; set; }
        public List<ProjectEntrepreneur> ProjectsEntrepreneurs { get; set; }
    }
}
