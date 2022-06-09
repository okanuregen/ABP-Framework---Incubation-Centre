using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Currencies
{
    public class GetCurrenciesInput : PagedAndSortedResultRequestDto
    {
        public string filter { get; set; }

        public string Country { get; set; }

        public string Title { get; set; }

        public string Symbol { get; set; }

        public bool IsDefault { get; set; }

        public bool FilterByIsDefault { get; set; }

        public GetCurrenciesInput()
        {

        }
    }
}
