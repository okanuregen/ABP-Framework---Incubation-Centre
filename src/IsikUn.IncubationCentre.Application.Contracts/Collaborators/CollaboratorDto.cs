using IsikUn.IncubationCentre.People;
namespace IsikUn.IncubationCentre.Collaborators
{
    public class CollaboratorDto : PersonDto
    {
        public virtual bool isActivated { get; set; }
    }
}
