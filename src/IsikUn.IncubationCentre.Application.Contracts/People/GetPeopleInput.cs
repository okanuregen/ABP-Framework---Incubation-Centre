using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.People
{
    public class GetPeopleInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public Guid? id { get; set; }
        public string About { get; set; }
        public Guid[] SkillIds { get; set; }
        public string Experience { get; set; }
        public Guid? IdentityUserId { get; set; }
        public GetPeopleInput()
        {

        }
    }
}
