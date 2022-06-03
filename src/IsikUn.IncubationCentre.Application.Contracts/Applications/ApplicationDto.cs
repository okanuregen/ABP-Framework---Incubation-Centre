using IsikUn.IncubationCentre.SystemManagers;
using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Applications
{
    [Serializable]
    public class ApplicationDto : FullAuditedEntityDto<Guid>
    {
        public ApplicationType MembershipType { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }

        public string SenderMail { get; set; }

        public string SenderPhoneNumber { get; set; }

        public string Explanation { get; set; }

        public SystemManagerDto Receiver { get; set; }

        public ApplicationStatus ApplicationStatus { get; set; }

        public Guid ReceiverId { get; set; }
    }
}