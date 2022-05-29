﻿using AutoMapper;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Skills;
using IsikUn.IncubationCentre.SystemManagers;

namespace IsikUn.IncubationCentre;

public class IncubationCentreApplicationAutoMapperProfile : Profile
{
    public IncubationCentreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<Skill, SkillDto>().ReverseMap();
        CreateMap<CreateSkillDto, Skill>().ReverseMap();
        CreateMap<CreateSkillDto, SkillDto>().ReverseMap();
        CreateMap<UpdateSkillDto, Skill>().ReverseMap();
        CreateMap<UpdateSkillDto, SkillDto>().ReverseMap();      
        
        CreateMap<Person, PersonDto>().ReverseMap();
        CreateMap<CreateUpdatePersonDto, PersonDto>().ReverseMap();
        CreateMap<CreateUpdatePersonDto, Person>().ReverseMap();

        CreateMap<Mentor, MentorDto>().ReverseMap();
        CreateMap<CreateUpdateMentorDto, MentorDto>().ReverseMap();
        CreateMap<CreateUpdateMentorDto, Mentor>().ReverseMap();

        CreateMap<Entrepreneur, EntrepreneurDto>().ReverseMap();
        CreateMap<CreateUpdateEntrepreneurDto, EntrepreneurDto>().ReverseMap();
        CreateMap<CreateUpdateEntrepreneurDto, Entrepreneur>().ReverseMap();

        CreateMap<Investor, InvestorDto>().ReverseMap();
        CreateMap<CreateUpdateInvestorDto, InvestorDto>().ReverseMap();
        CreateMap<CreateUpdateInvestorDto, Investor>().ReverseMap();

        CreateMap<SystemManager, SystemManagerDto>().ReverseMap();
        CreateMap<CreateUpdateSystemManagerDto, SystemManagerDto>().ReverseMap();
        CreateMap<CreateUpdateSystemManagerDto, SystemManager>().ReverseMap();

        CreateMap<Collaborator, CollaboratorDto>().ReverseMap();
        CreateMap<CreateUpdateCollaboratorDto, CollaboratorDto>().ReverseMap();
        CreateMap<CreateUpdateCollaboratorDto, Collaborator>().ReverseMap();

        CreateMap<Document, DocumentDto>().ReverseMap();
        CreateMap<CreateUpdateDocumentDto, DocumentDto>().ReverseMap();
        CreateMap<CreateUpdateDocumentDto, Document>().ReverseMap();

    }
}
