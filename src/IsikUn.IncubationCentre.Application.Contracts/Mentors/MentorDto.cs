using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Mentors
{
    public class MentorDto : PersonDto
    {
        public virtual bool isActivated { get; set; }
        public ICollection<ProjectDto> MentoringProjects { get; set; }

    }
}
