using IsikUn.IncubationCentre.People;

namespace IsikUn.IncubationCentre.Collaborators
{
    public class GetCollaboratorsInput : GetPeopleInput
    {
        public virtual bool isActivated { get; set; }
    }
}
