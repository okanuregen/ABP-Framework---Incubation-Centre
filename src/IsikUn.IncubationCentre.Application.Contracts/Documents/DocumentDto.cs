using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Documents
{
    [Serializable]
    public class DocumentDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string FullName { get; set; }

    }
}