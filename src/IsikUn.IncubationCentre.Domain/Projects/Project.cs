using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Events;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Milestones;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.ProjectsCollaborators;
using IsikUn.IncubationCentre.ProjectsEntrepreneurs;
using IsikUn.IncubationCentre.ProjectsFounders;
using IsikUn.IncubationCentre.ProjectsInvestors;
using IsikUn.IncubationCentre.ProjectsMentors;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Projects
{
    public class Project : FullAuditedEntity<Guid>
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
        public List<Document> Documents{ get; set; }
        public List<Milestone> Milestones{ get; set; }
        [JsonIgnore]
        public ICollection<Person> Founders { get; set; }
        [JsonIgnore]
        public List<ProjectFounder> ProjectsFounders { get; set; }
        [JsonIgnore]
        public ICollection<Investor> Investors { get; set; }
        [JsonIgnore]
        public List<ProjectInvestor> ProjectsInvestors { get; set; }
        [JsonIgnore]
        public ICollection<Mentor> Mentors { get; set; }
        [JsonIgnore]
        public List<ProjectMentor> ProjectsMentors { get; set; }
        [JsonIgnore]
        public ICollection<Collaborator> Collaborators { get; set; }
        [JsonIgnore]
        public List<ProjectCollaborator> ProjectsCollaborators { get; set; }
        public ICollection<Entrepreneur> Entrepreneurs { get; set; }
        public List<ProjectEntrepreneur> ProjectsEntrepreneurs { get; set; }

        public List<Event> Events { get; set; }


    }
}
