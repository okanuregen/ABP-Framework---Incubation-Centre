using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Events
{
    [Serializable]
    public class CreateUpdateEventDto
    {
        public string Title { get; set; }

        public DateTime EventDate { get; set; }

        public string Description { get; set; }

        public Guid? ProjectId { get; set; }

    }
}