using System;

using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Web.Pages.Events.Event.ViewModels
{
    public class CreateEventViewModel
    {
        [Display(Name = "EventTitle")]
        public string Title { get; set; }

        [Display(Name = "EventEventDate")]
        public DateTime EventDate { get; set; }

        [Display(Name = "EventDescription")]
        public string Description { get; set; }

        [Display(Name = "EventProjectId")]
        public Guid ProjectId { get; set; }

        [Display(Name = "EventProject")]
        public Project Project { get; set; }
    }
}