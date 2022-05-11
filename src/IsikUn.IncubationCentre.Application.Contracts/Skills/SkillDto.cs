using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.People;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Skills
{
    public class SkillDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<PersonDto> People { get; set; }
    }
}
