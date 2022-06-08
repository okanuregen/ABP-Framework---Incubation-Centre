using IsikUn.IncubationCentre.SystemManagers;
using System;
using System.ComponentModel;
namespace IsikUn.IncubationCentre.Applications
{
    [Serializable]
    public class CreateUpdateApplicationDto
    {
        public ApplicationType MembershipType { get; set; }
        public string SenderName { get; set; }
        public string SenderSurname { get; set; }
        public string SenderMail { get; set; }
        public string SenderPhoneNumber { get; set; }
        public string Explanation { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
    }
}