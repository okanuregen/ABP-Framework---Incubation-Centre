using IsikUn.IncubationCentre.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Requests
{
    public class Request : FullAuditedEntity<Guid>
    {
        public Guid SenderId { get; set; }
        public Person Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public Person Receiver { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
    }
}
