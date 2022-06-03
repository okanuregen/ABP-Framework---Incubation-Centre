using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Tasks
{
    [Serializable]
    public class CreateUpdateTaskDto
    {
        public Guid? AssignedToId { get; set; }

        public bool isDone { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}