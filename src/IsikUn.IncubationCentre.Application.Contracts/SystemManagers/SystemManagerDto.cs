using IsikUn.IncubationCentre.People;
namespace IsikUn.IncubationCentre.SystemManagers
{
    public class SystemManagerDto : PersonDto
    {
        public virtual bool isActivated { get; set; }
    }
}
