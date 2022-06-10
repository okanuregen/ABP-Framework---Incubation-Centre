using System;
using IsikUn.IncubationCentre.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Volo.Abp;
using System.Collections.Generic;
using System.Linq;
using IsikUn.IncubationCentre.Localization;
using IsikUn.IncubationCentre.ProjectsMentors;
using IsikUn.IncubationCentre.ProjectsInvestors;
using IsikUn.IncubationCentre.ProjectsCollaborators;
using IsikUn.IncubationCentre.ProjectsFounders;
using IsikUn.IncubationCentre.ProjectsEntrepreneurs;
using IsikUn.IncubationCentre.Mentors;

namespace IsikUn.IncubationCentre.Projects
{
    public class ProjectAppService : ApplicationService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectMentorRepository _projectMentorRepository;
        private readonly IProjectEntrepreneurRepository _projectEntreprenurRepository;
        private readonly IProjectInvestorRepository _projectInvestorRepository;
        private readonly IProjectCollaboratorRepository _projectCollaboratorRepository;
        private readonly IProjectFounderRepository _projectFounderRepository;

        private readonly IMentorRepository _mentorRepository;

        public ProjectAppService(
            IProjectRepository projectRepository,
            IProjectMentorRepository projectMentorRepository,
            IProjectEntrepreneurRepository projectEntreprenurRepository,
            IProjectInvestorRepository projectInvestorRepository,
            IProjectCollaboratorRepository projectCollaboratorRepository,
            IProjectFounderRepository projectFounderRepository,
            IMentorRepository mentorRepository
            )
        {
            this._projectRepository = projectRepository;
            this._projectMentorRepository = projectMentorRepository;
            this._projectEntreprenurRepository = projectEntreprenurRepository;
            this._projectInvestorRepository = projectInvestorRepository;
            this._projectCollaboratorRepository = projectCollaboratorRepository;
            this._projectFounderRepository = projectFounderRepository;
            this._mentorRepository = mentorRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Projects.Create)]
        public async Task<ProjectDto> CreateAsync(CreateUpdateProjectDto input)
        {
            var sameNameExist = await _projectRepository.FindAsync(a => a.Name == input.Name);
            if (sameNameExist != null)
            {
                throw new UserFriendlyException(L["ProjectNameAlreadyTaken"]);
            }
            var project = ObjectMapper.Map<CreateUpdateProjectDto, Project>(input);
            project.Status = ProjectStatus.InReview;
            project = await _projectRepository.InsertAsync(project, autoSave: true);

            if (input.EntreprenurId.HasValue)
            {
                await _projectEntreprenurRepository.InsertAsync(new ProjectEntrepreneur
                {
                    ProjectId = project.Id,
                    EntrepreneurId = input.EntreprenurId.Value
                },true);
            }
            if(input.CollaboratorIds != null && input.CollaboratorIds.Any())
            {
                var projectCollabs = input.CollaboratorIds.Select(a => new ProjectCollaborator
                {
                    ProjectId = project.Id,
                    CollaboratorId = Guid.Parse(a)
                });
                await _projectCollaboratorRepository.InsertManyAsync(projectCollabs,autoSave: true);
            }
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ProjectDto> ApproveProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project == null)
            {
                throw new UserFriendlyException(L["NoProjectFound"]);
            }
            project.Status = ProjectStatus.Approved;

            //Send Inform Mail To User
            project = await _projectRepository.UpdateAsync(project, autoSave: true);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ProjectDto> RejectProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            if (project == null)
            {
                throw new UserFriendlyException(L["NoProjectFound"]);
            }
            project.Status = ProjectStatus.Declined;
            //Send Inform Mail To User
            project = await _projectRepository.UpdateAsync(project, autoSave: true);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ProjectDto> AssignMentorAsync(Guid id, Guid mentorId)
        {
            var project = await _projectRepository.GetWithDetailAsync(id);
            if (project == null)
            {
                throw new UserFriendlyException(L["NoProjectFound"]);
            }
            if(project.Mentors != null && project.Mentors.Count(a => a.Id == mentorId) > 0)
            {
                throw new UserFriendlyException(L["MentorAlreadyAssingThisProject"]);
            }

            var mentor = await _mentorRepository.GetAsync(mentorId);
            if (mentor == null)
            {
                throw new UserFriendlyException(L["NoMentorFound"]);
            }
            await _projectMentorRepository.InsertAsync(new ProjectMentor
            {
                Project = project,
                ProjectId = project.Id,
                MentorId = mentorId,
                Mentor = mentor
            });
            project.Mentors.AddIfNotContains(mentor);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.Projects.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _projectCollaboratorRepository.DeleteManyAsync(await _projectCollaboratorRepository.GetListAsync(a => a.ProjectId == id));
            await _projectMentorRepository.DeleteManyAsync(await _projectMentorRepository.GetListAsync(a => a.ProjectId == id));
            await _projectInvestorRepository.DeleteManyAsync(await _projectInvestorRepository.GetListAsync(a => a.ProjectId == id));
            await _projectFounderRepository.DeleteManyAsync(await _projectFounderRepository.GetListAsync(a => a.ProjectId == id));
            await _projectRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Projects.Default)]
        public async Task<List<ProjectDto>> GetAllItemsAsync()
        {
            var items = (await _projectRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Project>, List<ProjectDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Projects.Default)]
        public async Task<ProjectDto> GetAsync(Guid id)
        {
            var project = await _projectRepository.GetAsync(id);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }


        [Authorize(IncubationCentrePermissions.Projects.Default)]
        public async Task<ProjectDto> GetWithDetailAsync(Guid id)
        {
            var project = await _projectRepository.GetWithDetailAsync(id);
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }


        [Authorize(IncubationCentrePermissions.Projects.Default)]
        public async Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectsInput input)
        {
            var totalCount = await _projectRepository.GetCountAsync(
                input.Status, 
                input.FiterByStatus,
                input.Filter,
                input.Name,
                input.Tags,
                input.FilterByInvesmentReady,
                input.InvesmentReady,
                input.FilterOpenForInvesment,
                input.OpenForInvesment,
                input.Founders != null ? input.Founders.Select(a => a.Id).ToList():null,
                input.Investors != null ? input.Investors.Select(a => a.Id).ToList() : null,
                input.Mentors != null ? input.Mentors.Select(a => a.Id).ToList() : null,
                input.Collaborators != null ? input.Collaborators.Select(a => a.Id).ToList() : null,
                input.Entrepreneurs,
                input.FilterByNoMentor,
                input.NoMentor);
            var items = await _projectRepository.GetListAsync(
                input.Status,
                input.FiterByStatus,
                input.Filter,
                input.Name,
                input.Tags,
                input.FilterByInvesmentReady,
                input.InvesmentReady,
                input.FilterOpenForInvesment,
                input.OpenForInvesment,
                input.Founders != null ? input.Founders.Select(a => a.Id).ToList() : null,
                input.Investors != null ? input.Investors.Select(a => a.Id).ToList() : null,
                input.Mentors != null ? input.Mentors.Select(a => a.Id).ToList() : null,
                input.Collaborators != null ? input.Collaborators.Select(a => a.Id).ToList() : null,
                input.Entrepreneurs,
                input.FilterByNoMentor,
                input.NoMentor,
                input.Sorting, input.SkipCount, input.MaxResultCount);

            return new PagedResultDto<ProjectDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Project>, List<ProjectDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Projects.Edit)]
        public async Task<ProjectDto> UpdateAsync(Guid id, CreateUpdateProjectDto input)
        {
            var sameNameExist = await _projectRepository.FindAsync(a => a.Name == input.Name && a.Id != id);
            if (sameNameExist != null)
            {
                throw new UserFriendlyException(L["ProjectNameAlreadyTaken"]);
            }

            var project = await _projectRepository.GetAsync(id);
            ObjectMapper.Map(input, project);
            project = await _projectRepository.UpdateAsync(project, autoSave: true);

            await _projectEntreprenurRepository.DeleteAsync(a => a.ProjectId == id);
            if (input.EntreprenurId.HasValue)
            {
                await _projectEntreprenurRepository.InsertAsync(new ProjectEntrepreneur
                {
                    ProjectId = project.Id,
                    EntrepreneurId = input.EntreprenurId.Value
                }, true);
            }
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }
    }
}
