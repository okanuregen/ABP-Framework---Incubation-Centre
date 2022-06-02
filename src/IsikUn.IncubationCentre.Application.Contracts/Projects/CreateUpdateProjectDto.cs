using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Projects
{
    [Serializable]
    public class CreateUpdateProjectDto
    {
        [Required]
        public string Name { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? CompletionDate { get; set; }
        /// <summary>
        /// comma seperated string
        /// </summary>
        public string Tags { get; set; }
        public bool InvesmentReady { get; set; }
        public bool OpenForInvesment { get; set; }
        [Range(0,100)]
        public double SharePerInvest { get; set; }
        public int TotalValuation { get; set; }
        public ProjectStatus Status { get; set; }
        
    }
}