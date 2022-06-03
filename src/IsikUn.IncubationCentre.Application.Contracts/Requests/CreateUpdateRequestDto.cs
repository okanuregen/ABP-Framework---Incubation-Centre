using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Requests
{
    [Serializable]
    public class CreateUpdateRequestDto
    {
        public Guid SenderId { get; set; }

        public Guid ReceiverId { get; set; }

        public string Title { get; set; }

        public string Explanation { get; set; }
    }
}