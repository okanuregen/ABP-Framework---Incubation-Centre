using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.People
{
    public abstract class CreateUpdatePersonDto : FullAuditedEntityDto<Guid>
    {
        public virtual string About { get; set; }
        public virtual List<Guid> SkillIds { get; set; }
        public virtual string Experience { get; set; }
        public virtual Guid IdentityUserId { get; set; }
    }
}
