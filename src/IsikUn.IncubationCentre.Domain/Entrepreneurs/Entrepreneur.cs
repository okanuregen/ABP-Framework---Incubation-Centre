﻿using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    public class Entrepreneur : Person
    {
        public virtual bool isActivated { get; set; }
    }
}
