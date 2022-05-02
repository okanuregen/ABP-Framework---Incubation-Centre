using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Skills
{
    public class UpdateSkillDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Category { get; set; }
    }
}