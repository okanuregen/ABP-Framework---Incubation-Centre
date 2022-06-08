using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Requests
{
    [Serializable]
    public class CreateUpdateRequestDto
    {
        public virtual Guid? SenderId { get; set; }

        public virtual Guid? ReceiverId { get; set; }

        public virtual string Title { get; set; }

        public virtual string Explanation { get; set; }
    }
}