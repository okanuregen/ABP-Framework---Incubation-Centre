using IsikUn.IncubationCentre.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Documents
{
    public class Document : FullAuditedEntity<Guid> 
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public Project Project { get; set; }
        public Guid ProjectId { get; set; }
    }
}
