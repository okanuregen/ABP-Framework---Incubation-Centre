using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
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
        public List<EntrepreneurDto> Entrepreneurs { get; set; }
        public List<InvestorDto> Investors { get; set; }
        public List<MentorDto> Mentors { get; set; }
        public List<CollaboratorDto> Collaborators { get; set; }
    }
}
