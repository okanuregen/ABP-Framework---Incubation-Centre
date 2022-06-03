using IsikUn.IncubationCentre.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Tasks
{
    public class Task : FullAuditedEntity<Guid>
    {
        public Guid? AssignedToId { get; set; }
        public Person AssignedTo { get; set; }
        public bool isDone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? CompletionDate { get; set; }
    }
}
