using System;
using Volo.Abp.Application.Dtos;

namespace IsikUn.IncubationCentre.Currencies
{
    [Serializable]
    public class CurrencyDto : FullAuditedEntityDto<Guid>
    {
        public string Country { get; set; }

        public string Title { get; set; }

        public string Symbol { get; set; }

        public bool IsDefault { get; set; }
    }
}