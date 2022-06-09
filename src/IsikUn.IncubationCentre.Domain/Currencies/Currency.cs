using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Currencies
{
    public class Currency : FullAuditedEntity<Guid>
    {
        public string Country { get; set; }
        public string Title { get; set; }
        public string Symbol { get; set; }
        public bool IsDefault { get; set; }
     
    }
}
