using IsikUn.IncubationCentre.People;
using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Requests
{
    [Serializable]
    public class RequestDto : FullAuditedEntityDto<Guid>
    {
        public Guid? SenderId { get; set; }

        public PersonDto Sender { get; set; }

        public Guid? ReceiverId { get; set; }

        public PersonDto Receiver { get; set; }

        public string Title { get; set; }

        public string Explanation { get; set; }
    }
}