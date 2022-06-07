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

        /// <summary>
        /// comma seperated string
        /// </summary>
        public string Tags { get; set; }
        [Range(0,100)]
        public double SharePerInvest { get; set; }
        [Range(0,Double.MaxValue,ErrorMessage = "This field cannot be negative")]
        public int TotalValuation { get; set; }
        public Guid? EntreprenurId { get; set; }
        
    }
}