using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    public class EntrepreneurDto : FullAuditedEntityDto<Guid>
    {
        public virtual bool isActivated { get; set; }
        public string About { get; set; }
        public List<SkillDto> Skills { get; set; }
        public string Experience { get; set; }
        public Guid? IdentityUserId { get; set; }

        public IdentityUserDto IdentityUser { get; set; }
    }
}
