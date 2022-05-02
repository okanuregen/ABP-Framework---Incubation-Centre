using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Skills
{
    public class Skill : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Entrepreneur> Entrepreneurs { get; set; }
        public List<Investor> Investors { get; set; }
        public List<Mentor> Mentors { get; set; }
        public List<Collaborator> Collaborators { get; set; }

    }
}
