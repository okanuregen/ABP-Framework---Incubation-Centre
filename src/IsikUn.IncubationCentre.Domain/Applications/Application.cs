using IsikUn.IncubationCentre.SystemManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace IsikUn.IncubationCentre.Applications
{
    public class Application : FullAuditedEntity<Guid>
    {
        public ApplicationType MembershipType { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderMail { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string Explanation { get; set; }
        public Guid? ReceiverId { get; set; }
        public SystemManager Receiver { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}
