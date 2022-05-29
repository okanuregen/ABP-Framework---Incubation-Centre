using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Documents
{
    public class GetDocumentsInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }


        public GetDocumentsInput()
        {

        }
    }
}
