using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Mentors
{
    public class Mentor : FullAuditedEntity<Guid>
    {
        public virtual bool isActivated { get; set; }
        public string About { get; set; }
        public List<Skill> Skills { get; set; }
        public string Experience { get; set; }
        public Guid? IdentityUserId { get; set; }

        public IdentityUser IdentityUser { get; set; }
    }
}
