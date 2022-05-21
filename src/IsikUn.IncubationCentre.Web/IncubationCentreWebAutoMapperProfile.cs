using AutoMapper;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Skills;
using static IsikUn.IncubationCentre.Web.Pages.Mentors.CreateModalModel;

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

        CreateMap<Mentor, MentorDto>().ReverseMap();
        CreateMap<CreateUpdateMentorDto, MentorDto>().ReverseMap();
        CreateMap<CreateUpdateMentorDto, Mentor>().ReverseMap();
        CreateMap<CreateMentorViewModel, CreateUpdateMentorDto>().ReverseMap();
    }
}
