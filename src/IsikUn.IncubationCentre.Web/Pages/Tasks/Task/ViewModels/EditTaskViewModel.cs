using System;

using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Web.Pages.Tasks.Task.ViewModels
{
    public class EditTaskViewModel
    {
        [Display(Name = "TaskAssignedToId")]
        public Guid AssignedToId { get; set; }

        [Display(Name = "TaskAssignedTo")]
        public Person AssignedTo { get; set; }

        [Display(Name = "TaskisDone")]
        public bool isDone { get; set; }

        [Display(Name = "TaskTitle")]
        public string Title { get; set; }

        [Display(Name = "TaskDescription")]
        public string Description { get; set; }

        [Display(Name = "TaskCompletionDate")]
        public DateTime? CompletionDate { get; set; }
    }
}