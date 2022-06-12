using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Events
{
    public class GetEventsInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ProjectName { get; set; }
        public string CreatorUserName { get; set; }
        public Guid[] projectIds { get; set; }


        public GetEventsInput()
        {

        }
    }
}
