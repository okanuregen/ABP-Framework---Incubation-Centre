using IsikUn.IncubationCentre.People;


namespace IsikUn.IncubationCentre.Collaborators
{
    public class CreateUpdateCollaboratorDto : CreateUpdatePersonDto
    {
        public virtual bool isActivated { get; set; }
    }
}
