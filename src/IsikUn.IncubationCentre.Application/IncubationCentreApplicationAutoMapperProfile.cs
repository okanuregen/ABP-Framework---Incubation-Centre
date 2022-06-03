using AutoMapper;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Milestones;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Skills;
using IsikUn.IncubationCentre.Applications;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Requests.Dtos;
using IsikUn.IncubationCentre.Tasks;
using IsikUn.IncubationCentre.Tasks.Dtos;
using IsikUn.IncubationCentre.Events;
using IsikUn.IncubationCentre.Events.Dtos;
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

        CreateMap<Project, ProjectDto>().ReverseMap();
        CreateMap<CreateUpdateProjectDto, ProjectDto>().ReverseMap();
        CreateMap<CreateUpdateProjectDto, Project>().ReverseMap();

        CreateMap<Milestone, MilestoneDto>().ReverseMap();
        CreateMap<CreateUpdateMilestoneDto, MilestoneDto>().ReverseMap();
        CreateMap<CreateUpdateMilestoneDto, Milestone>().ReverseMap();

        CreateMap<Application, ApplicationDto>().ReverseMap();
        CreateMap<CreateUpdateApplicationDto, ApplicationDto>().ReverseMap();
        CreateMap<CreateUpdateApplicationDto, Application>().ReverseMap();

        CreateMap<Request, RequestDto>().ReverseMap();
        CreateMap<CreateUpdateRequestDto, RequestDto>().ReverseMap();
        CreateMap<CreateUpdateRequestDto, Request>().ReverseMap();

        CreateMap<Task, TaskDto>().ReverseMap();
        CreateMap<CreateUpdateTaskDto, TaskDto>().ReverseMap();
        CreateMap<CreateUpdateTaskDto, Task>().ReverseMap();

        CreateMap<Event, EventDto>().ReverseMap();
        CreateMap<CreateUpdateEventDto, EventDto>().ReverseMap();
        CreateMap<CreateUpdateEventDto, Event>().ReverseMap();
    }
}
