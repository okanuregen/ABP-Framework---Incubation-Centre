using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Events
{
    [Serializable]
    public class CreateUpdateEventDto
    {
        [Required]
        public string Title { get; set; }

        public DateTime EventDate { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public virtual Guid ProjectId { get; set; }

    }
}