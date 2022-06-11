using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Milestones
{
    public class GetMilestonesInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string Title { get; set; }
        public string SuccessCriteria { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? isCompleted { get; set; }
        public bool filterByIsComleted { get; set; }
        public Guid? ProjectId { get; set; }


        public GetMilestonesInput()
        {

        }
    }
}
