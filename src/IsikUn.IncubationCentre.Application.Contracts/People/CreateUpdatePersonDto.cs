using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.People
{
    public abstract class CreateUpdatePersonDto<T> : FullAuditedEntityDto<T>
    {
        public string About { get; set; }
        public Guid[] SkillIds { get; set; }
        public string Experience { get; set; }
        public Guid? IdentityUserId { get; set; }
    }
}
