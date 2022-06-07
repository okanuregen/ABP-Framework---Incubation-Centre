using IsikUn.IncubationCentre.Localization;
using IsikUn.IncubationCentre.Milestones;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Permissions;
using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    [Authorize(IncubationCentrePermissions.Entrepreneurs.Default)]
    public class EntrepreneurAppService : ApplicationService, IEntrepreneurAppService
    {
        private readonly IEntrepreneurRepository _entrepreneurRepository;
        private readonly IPersonSkillRepository _personSkillRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public EntrepreneurAppService(
            IEntrepreneurRepository entrepreneurRepository, 
            IIdentityUserRepository identityUserRepository,
            IPersonSkillRepository personSkillRepository
            )
        {
            this._entrepreneurRepository = entrepreneurRepository;
            this._identityUserRepository = identityUserRepository;
            this._personSkillRepository = personSkillRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Create)]
        public async Task<EntrepreneurDto> CreateAsync(CreateUpdateEntrepreneurDto input)
        {
            var entrepreneur = ObjectMapper.Map<CreateUpdateEntrepreneurDto, Entrepreneur>(input);
            entrepreneur = await _entrepreneurRepository.InsertAsync(entrepreneur, autoSave: true);
            return ObjectMapper.Map<Entrepreneur, EntrepreneurDto>(entrepreneur);
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _personSkillRepository.DeleteManyAsync(await _personSkillRepository.GetListAsync(a => a.PersonId == id, true));
            await _entrepreneurRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Default)]
        public async Task<List<EntrepreneurDto>> GetAllItemsAsync()
        {
            var items = (await _entrepreneurRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Entrepreneur>, List<EntrepreneurDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Default)]
        public async Task<EntrepreneurDto> GetAsync(Guid id)
        {
            var entrepreneur = await _entrepreneurRepository.GetAsync(id);
            return ObjectMapper.Map<Entrepreneur, EntrepreneurDto>(entrepreneur);
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Default)]
        public async Task<PagedResultDto<EntrepreneurDto>> GetListAsync(GetEntrepreneursInput input)
        {
            long totalCount = 0;
            List<Entrepreneur> items = null;
            if (input.IdentityUserId.HasValue)
            {
                var IdentityUser = await _identityUserRepository.GetAsync(input.IdentityUserId.Value);
                totalCount = await _entrepreneurRepository.GetCountAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false);
                items = await _entrepreneurRepository.GetListAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false, true, input.SkipCount, input.MaxResultCount, input.Sorting);
            }
            else
            {
                totalCount = await _entrepreneurRepository.GetCountAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted:false);
                items = await _entrepreneurRepository.GetListAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted: false, skipCount: input.SkipCount, maxResultCount: input.MaxResultCount, sorting: input.Sorting);
            }
            return new PagedResultDto<EntrepreneurDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Entrepreneur>, List<EntrepreneurDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Default)]
        public async Task<PagedResultDto<ProjectDto>> GetProjectListAsync(GetEntrepreneursInput input)
        {
            var entreprenur =  await _entrepreneurRepository.GetWithDetailAsync(input.id.Value);
            List<Project> projects = entreprenur.MyProjects != null ? entreprenur.MyProjects.ToList() : null;
            return new PagedResultDto<ProjectDto>
            {
                TotalCount = entreprenur.MyProjects != null ? entreprenur.MyProjects.Count : 0,
                Items = ObjectMapper.Map<List<Project>, List<ProjectDto>>(projects)
            };
        }


        [Authorize(IncubationCentrePermissions.Entrepreneurs.Default)]
        public async Task<PagedResultDto<MilestoneDto>> GetMilestoneListAsync(GetEntrepreneursInput input)
        {
            var entreprenur = await _entrepreneurRepository.GetWithDetailAsync(input.id.Value);
            List<Project> projects = entreprenur.MyProjects != null ? entreprenur.MyProjects.ToList() : null;
            if(projects == null)
            {
                return new PagedResultDto<MilestoneDto>
                {
                    TotalCount = 0,
                    Items = new List<MilestoneDto>()
                };
            }
            List<Milestone> milestones = new List<Milestone>();
            projects.ForEach(a => a.Milestones.ForEach(b => {
                b.Project = a;
                milestones.Add(b);
            }));

            return new PagedResultDto<MilestoneDto>
            {
                TotalCount = milestones.Count(),
                Items = ObjectMapper.Map<List<Milestone>, List<MilestoneDto>>(milestones.OrderBy(a => a.Project.Name).ToList()) 
            };
        }

        [Authorize(IncubationCentrePermissions.Entrepreneurs.Edit)]
        public async Task<EntrepreneurDto> UpdateAsync(Guid id, CreateUpdateEntrepreneurDto input)
        {
            var entrepreneur = await _entrepreneurRepository.GetAsync(id);
            ObjectMapper.Map(input, entrepreneur);
            entrepreneur = await _entrepreneurRepository.UpdateAsync(entrepreneur, autoSave: true);
            entrepreneur = await _entrepreneurRepository.GetWithDetailAsync(id);
            return ObjectMapper.Map<Entrepreneur, EntrepreneurDto>(entrepreneur);

        }
    }
}
