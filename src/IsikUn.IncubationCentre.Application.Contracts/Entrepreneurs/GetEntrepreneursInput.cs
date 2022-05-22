using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    public class GetEntrepreneursInput : GetPeopleInput
    {
        public virtual bool isActivated { get; set; }
    }
}
