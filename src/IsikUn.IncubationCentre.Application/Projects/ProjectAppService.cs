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
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.People;
using Volo.Abp.Emailing;

namespace IsikUn.IncubationCentre.Projects
{
    public class ProjectAppService : ApplicationService, IProjectAppService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IInvestorRepository _investorRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IProjectMentorRepository _projectMentorRepository;
        private readonly IProjectEntrepreneurRepository _projectEntreprenurRepository;
        private readonly IProjectInvestorRepository _projectInvestorRepository;
        private readonly IProjectCollaboratorRepository _projectCollaboratorRepository;
        private readonly IProjectFounderRepository _projectFounderRepository;
        private readonly IEmailSender _emailSender;

        private readonly IMentorRepository _mentorRepository;

        public ProjectAppService(
            IProjectRepository projectRepository,
            IInvestorRepository investorRepository,
            IPersonRepository personRepository,
            IProjectMentorRepository projectMentorRepository,
            IProjectEntrepreneurRepository projectEntreprenurRepository,
            IProjectInvestorRepository projectInvestorRepository,
            IProjectCollaboratorRepository projectCollaboratorRepository,
            IProjectFounderRepository projectFounderRepository,
            IMentorRepository mentorRepository,
            IEmailSender emailSender
            )
        {
            this._projectRepository = projectRepository;
            this._investorRepository = investorRepository;
            this._projectMentorRepository = projectMentorRepository;
            this._projectEntreprenurRepository = projectEntreprenurRepository;
            this._projectInvestorRepository = projectInvestorRepository;
            this._projectCollaboratorRepository = projectCollaboratorRepository;
            this._projectFounderRepository = projectFounderRepository;
            this._mentorRepository = mentorRepository;
            this._emailSender = emailSender;
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
                }, true);
            }
            if (input.CollaboratorIds != null && input.CollaboratorIds.Any())
            {
                var projectCollabs = input.CollaboratorIds.Select(a => new ProjectCollaborator
                {
                    ProjectId = project.Id,
                    CollaboratorId = Guid.Parse(a)
                });
                await _projectCollaboratorRepository.InsertManyAsync(projectCollabs, autoSave: true);
            }
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ProjectDto> ApproveProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetWithDetailAsync(id);
            if (project == null)
            {
                throw new UserFriendlyException(L["NoProjectFound"]);
            }
            project.Status = ProjectStatus.Approved;



            project = await _projectRepository.UpdateAsync(project, autoSave: true);
            //Send Inform Mail To User
            foreach (var mail in project.Collaborators.Select(a => a.IdentityUser.Email))
            {
                try
                {
                    await _emailSender.SendAsync(
                        mail,
                        @L["ProjectApplication"],
                        @L["ProjectApprovedMail"]
                        );
                }
                catch
                {

                }
            }
            foreach (var mail in project.Entrepreneurs.Select(a => a.IdentityUser.Email))
            {
                try
                {
                    await _emailSender.SendAsync(
                        mail,
                        @L["ProjectApplication"],
                        @L["ProjectApprovedMail"]
                        );
                }
                catch
                {

                }
            }
            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ProjectDto> RejectProjectAsync(Guid id)
        {
            var project = await _projectRepository.GetWithDetailAsync(id);
            if (project == null)
            {
                throw new UserFriendlyException(L["NoProjectFound"]);
            }
            project.Status = ProjectStatus.Declined;


            project = await _projectRepository.UpdateAsync(project, autoSave: true);
            //Send Inform Mail To User
            foreach (var mail in project.Collaborators.Select(a => a.IdentityUser.Email))
            {
                try
                {
                    await _emailSender.SendAsync(
                        mail,
                        @L["ProjectApplication"],
                        @L["ProjectRejectedMail"]
                        );
                }
                catch
                {

                }
            }
            foreach (var mail in project.Entrepreneurs.Select(a => a.IdentityUser.Email))
            {
                try
                {
                    await _emailSender.SendAsync(
                        mail,
                        @L["ProjectApplication"],
                        @L["ProjectRejectedMail"]
                        );
                }
                catch
                {

                }
            }
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
            if (project.Mentors != null && project.Mentors.Count(a => a.Id == mentorId) > 0)
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
            //Information mail to project crew

            foreach (var mail in project.Collaborators.Select(a => a.IdentityUser.Email))
            {
                try
                {
                    await _emailSender.SendAsync(
                        mail,
                        @L["ProjectMentorAssgn"],
                        @L["ProjectMentorAssgnMail"]
                        );
                }
                catch
                {

                }
            }
            foreach (var mail in project.Entrepreneurs.Select(a => a.IdentityUser.Email))
            {
                try
                {
                    await _emailSender.SendAsync(
                        mail,
                        @L["ProjectApplication"],
                        @L["ProjectMentorAssgnMail"]
                        );
                }
                catch
                {

                }
            }

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

        public async Task Invest(Guid projectId)
        {
            var project = await _projectRepository.GetWithDetailAsync(projectId);
            if (!(project.InvesmentReady && project.OpenForInvesment))
            {
                throw new UserFriendlyException(L["ProjectIsNotSuitableForInvesment"]);
            }
            var investor = await _investorRepository.GetAsync(a => a.IdentityUserId == CurrentUser.Id.Value);

            var projectShareHoldersTotal = await _projectInvestorRepository.GetListAsync(ProjectId: projectId);
            if (projectShareHoldersTotal.Select(x => x.Share).Sum() + project.SharePerInvest > 100)
            {
                throw new UserFriendlyException(L["SharedProjectIsOvering100Percantage"]);
            }
            var lastInvest = await _projectInvestorRepository.FindAsync(a => a.ProjectId == projectId && a.InvestorId == investor.Id);
            if (lastInvest == null)
            {
                await _projectInvestorRepository.InsertAsync(new ProjectInvestor
                {
                    ProjectId = projectId,
                    InvestorId = investor.Id,
                    Share = project.SharePerInvest
                });
            }
            else
            {

                lastInvest.Share += project.SharePerInvest;
                await _projectInvestorRepository.UpdateAsync(lastInvest);
            }
            //Information mail to user
            if (project.Collaborators != null)
            {

                foreach (var mail in project.Collaborators.Select(a => a.IdentityUser.Email))
                {
                    try
                    {
                        await _emailSender.SendAsync(
                            mail,
                            @L["ProjectInvestorAbout"],
                            @L["ProjectInvestorAssgn"]
                            );
                    }
                    catch
                    {

                    }
                }
            }
            if (project.Entrepreneurs != null)
            {
                foreach (var mail in project.Entrepreneurs.Select(a => a.IdentityUser.Email))
                {
                    try
                    {
                        await _emailSender.SendAsync(
                            mail,
                            @L["ProjectInvestorAbout"],
                            @L["ProjectInvestorAssgn"]
                            );
                    }
                    catch
                    {

                    }
                }

            }
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
                input.projectIds,
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
                input.NoMentor);
            var items = await _projectRepository.GetListAsync(
                input.Status,
                input.projectIds,
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

            var project = await _projectRepository.GetWithDetailAsync(id);
            input.OpenForInvesment = input.InvesmentReady == false ? false : input.OpenForInvesment;

            if (input.Status == ProjectStatus.OnGoing)//check any of the co founders (collabs and entre if has ongoing project)
            {
                if (project.Collaborators.Count(a => a.CollaboratoringProjects.Count(b => b.Status == ProjectStatus.OnGoing && b.Id != id) > 0) > 0)
                {
                    throw new UserFriendlyException(L["CoFounderOfProjectsAlreadyHasOngoingProject"]);
                }
                if (project.Entrepreneurs.Count(a => a.MyProjects.Count(b => b.Status == ProjectStatus.OnGoing && b.Id != id) > 0) > 0)
                {
                    throw new UserFriendlyException(L["CoFounderOfProjectsAlreadyHasOngoingProject"]);
                }
            }


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

            await _projectCollaboratorRepository.DeleteAsync(a => a.ProjectId == id);
            if (input.CollaboratorIds != null && input.CollaboratorIds.Any())
            {
                var projectCollabs = input.CollaboratorIds.Select(a => new ProjectCollaborator
                {
                    ProjectId = project.Id,
                    CollaboratorId = Guid.Parse(a)
                });
                await _projectCollaboratorRepository.InsertManyAsync(projectCollabs, autoSave: true);
            }

            return ObjectMapper.Map<Project, ProjectDto>(project);
        }

        [Authorize(IncubationCentrePermissions.Projects.Default)]
        public async Task<PagedResultDto<ProjectDto>> GetListByInvestorAsync(GetInvestorsInput investor, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            var investProject = await _projectInvestorRepository.GetListAsync(InvestorId: investor.id);
            if (investProject == null || !investProject.Any())
            {
                return new PagedResultDto<ProjectDto>
                {
                    TotalCount = 0,
                    Items = null
                };
            }

            return await GetListAsync(new GetProjectsInput
            {
                projectIds = investProject.Select(a => a.ProjectId).ToArray(),
                Sorting = sorting,
                SkipCount = skipCount,
                MaxResultCount = maxResultCount
            });

        }

        [Authorize(IncubationCentrePermissions.Projects.Default)]
        public async Task<PagedResultDto<ProjectDto>> GetListByMentorAsync(GetMentorsInput mentor, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue)
        {
            var mentoringProject = await _projectMentorRepository.GetListAsync(MentorId: mentor.id);
            if (mentoringProject == null || !mentoringProject.Any())
            {
                return new PagedResultDto<ProjectDto>
                {
                    TotalCount = 0,
                    Items = null
                };
            }

            return await GetListAsync(new GetProjectsInput
            {
                projectIds = mentoringProject.Select(a => a.ProjectId).ToArray(),
                Sorting = sorting,
                SkipCount = skipCount,
                MaxResultCount = maxResultCount
            });
        }

    }
}
