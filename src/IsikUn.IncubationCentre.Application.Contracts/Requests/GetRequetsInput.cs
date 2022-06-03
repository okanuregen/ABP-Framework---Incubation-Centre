using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Requests
{
    public class GetRequetsInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }
        public string SenderUserName { get; set; }
        public string ReceiverUserName { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }


        public GetRequetsInput()
        {

        }
    }
}
