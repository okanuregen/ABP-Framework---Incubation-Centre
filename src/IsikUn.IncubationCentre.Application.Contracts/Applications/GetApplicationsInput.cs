using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Applications
{
    public class GetApplicationsInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderMail { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string Explanation { get; set; }
        public string ApplicationStatus { get; set; }

    }
}
