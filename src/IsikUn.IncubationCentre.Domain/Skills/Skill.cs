using IsikUn.IncubationCentre.People;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Skills
{
    public class Skill : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<Person> People { get; set; }

    }
}
