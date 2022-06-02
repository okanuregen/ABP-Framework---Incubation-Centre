using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Milestones
{
    [Serializable]
    public class CreateUpdateMilestoneDto
    {
        public string Title { get; set; }
        public string SuccessCriteria { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isCompleted { get; set; }
        public Guid ProjectId { get; set; }

    }
}