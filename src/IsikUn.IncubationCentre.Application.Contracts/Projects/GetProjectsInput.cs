using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.People;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Projects
{
    public class GetProjectsInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
        public string Name { get; set; }
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// comma seperated string
        /// </summary>
        public string Tags { get; set; }
        public bool InvesmentReady { get; set; }
        public bool FilterByInvesmentReady { get; set; }
        public bool OpenForInvesment { get; set; }
        public bool FilterOpenForInvesment { get; set; }
        public double SharePerInvest { get; set; }
        public int TotalValuation { get; set; }
        public ProjectStatus Status { get; set; }
        public bool FiterByStatus { get; set; }
        public ICollection<PersonDto> Founders { get; set; }
        public ICollection<InvestorDto> Investors { get; set; }
        public ICollection<MentorDto> Mentors { get; set; }
        public ICollection<CollaboratorDto> Collaborators { get; set; }
        public Guid[] Entrepreneurs { get; set; }
        public bool FilterByNoMentor { get; set; }
        public bool NoMentor { get; set; }
        public GetProjectsInput()
        {

        }
    }
}
