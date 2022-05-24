using AutoMapper;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Skills;
using IsikUn.IncubationCentre.SystemManagers;
using static IsikUn.IncubationCentre.Web.Pages.Collaborators.CreateModalModel;
using static IsikUn.IncubationCentre.Web.Pages.Entrepreneurs.CreateModalModel;
using static IsikUn.IncubationCentre.Web.Pages.Investors.CreateModalModel;
using static IsikUn.IncubationCentre.Web.Pages.Mentors.CreateModalModel;
using static IsikUn.IncubationCentre.Web.Pages.SystemManagers.CreateModalModel;

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

        CreateMap<Entrepreneur, EntrepreneurDto>().ReverseMap();
        CreateMap<CreateUpdateEntrepreneurDto, EntrepreneurDto>().ReverseMap();
        CreateMap<CreateUpdateEntrepreneurDto, Entrepreneur>().ReverseMap();
        CreateMap<CreateEntrepreneurViewModel, CreateUpdateEntrepreneurDto>().ReverseMap();

        CreateMap<Investor, InvestorDto>().ReverseMap();
        CreateMap<CreateUpdateInvestorDto, InvestorDto>().ReverseMap();
        CreateMap<CreateUpdateInvestorDto, Investor>().ReverseMap();
        CreateMap<CreateInvestorViewModel, CreateUpdateInvestorDto>().ReverseMap();

        CreateMap<SystemManager, SystemManagerDto>().ReverseMap();
        CreateMap<CreateUpdateSystemManagerDto, SystemManagerDto>().ReverseMap();
        CreateMap<CreateUpdateSystemManagerDto, SystemManager>().ReverseMap();
        CreateMap<CreateSystemManagerViewModel, CreateUpdateSystemManagerDto>().ReverseMap();

        CreateMap<Collaborator, CollaboratorDto>().ReverseMap();
        CreateMap<CreateUpdateCollaboratorDto, CollaboratorDto>().ReverseMap();
        CreateMap<CreateUpdateCollaboratorDto, Collaborator>().ReverseMap();
        CreateMap<CreateCollaboratorViewModel, CreateUpdateCollaboratorDto>().ReverseMap();
    }
}
