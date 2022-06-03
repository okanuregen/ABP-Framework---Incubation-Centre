using IsikUn.IncubationCentre.Applications;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.SystemManagers
{
    public class SystemManager : Person
    {
        public virtual bool isActivated { get; set; }
        public List<Application> Applications { get; set; }
    }
}
