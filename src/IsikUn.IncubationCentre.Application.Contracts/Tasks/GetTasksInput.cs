using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Tasks
{
    public class GetTasksInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string AssignedUserName { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public GetTasksInput()
        {

        }
    }
}
