﻿using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.SystemManagers
{
    public class SystemManager : FullAuditedEntity<Guid>
    {
        public Guid? IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}
