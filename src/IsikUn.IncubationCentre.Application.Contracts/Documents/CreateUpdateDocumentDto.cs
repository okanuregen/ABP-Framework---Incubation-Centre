using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Documents
{
    [Serializable]
    public class CreateUpdateDocumentDto
    {
        public string Name { get; set; }
        public string FullName { get; set; }

    }
}