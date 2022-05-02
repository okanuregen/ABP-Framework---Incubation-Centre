using AutoMapper;
using IsikUn.IncubationCentre.Skills;

namespace IsikUn.IncubationCentre.Web;

public class IncubationCentreWebAutoMapperProfile : Profile
{
    public IncubationCentreWebAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Web project.
        CreateMap<Skill, SkillDto>().ReverseMap();
        CreateMap<CreateSkillDto, Skill>().ReverseMap();
        CreateMap<CreateSkillDto, SkillDto>().ReverseMap();
        CreateMap<UpdateSkillDto, Skill>().ReverseMap();
        CreateMap<UpdateSkillDto, SkillDto>().ReverseMap();
    }
}
