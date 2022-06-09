using System;
using System.ComponentModel;

namespace IsikUn.IncubationCentre.Currencies
{
    [Serializable]
    public class CreateUpdateCurrencyDto
    {
        public string Country { get; set; }

        public string Title { get; set; }

        public string Symbol { get; set; }

        public bool IsDefault { get; set; }
    }
}