using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Milestones;
using IsikUn.IncubationCentre.People;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Projects
{
    [Serializable]
    public class ProjectDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// comma seperated string
        /// </summary>
        public string Tags { get; set; }
        public bool InvesmentReady { get; set; }
        public bool OpenForInvesment { get; set; }
        public double SharePerInvest { get; set; }
        public int TotalValuation { get; set; }
        public ProjectStatus Status { get; set; }
        public List<DocumentDto> Documents { get; set; }
        public List<MilestoneDto> Milestones { get; set; }
        public ICollection<PersonDto> Founders { get; set; }
        public ICollection<InvestorDto> Investors { get; set; }
        public ICollection<MentorDto> Mentors { get; set; }
        public ICollection<CollaboratorDto> Collaborators { get; set; }

    }
}