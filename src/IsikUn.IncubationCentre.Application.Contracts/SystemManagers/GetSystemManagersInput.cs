using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.SystemManagers
{
    public class GetSystemManagersInput : GetPeopleInput
    {
        public virtual bool isActivated { get; set; }
    }
}
