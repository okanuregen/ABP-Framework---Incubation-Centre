using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.PeopleSkills;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Skills
{
    public class Skill : FullAuditedEntity<Guid>
    {
        public string Name { get; set; }
        public string Category { get; set; }

        [JsonIgnore]
        public ICollection<Person> People { get; set; }

        [JsonIgnore]
        public List<PersonSkill> PeopleSkills { get; set; }

    }
}
