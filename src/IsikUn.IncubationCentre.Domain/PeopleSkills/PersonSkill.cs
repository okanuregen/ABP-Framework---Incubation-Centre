using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.PeopleSkills
{
    public class PersonSkill : AuditedEntity<Guid>
    {
        public Guid PersonId { get; set; }
        public Guid SkillId { get; set; }
        public Skill Skill { get; set; }
        public Person Person { get; set; }
    }
}
