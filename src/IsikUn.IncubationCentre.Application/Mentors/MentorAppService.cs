using IsikUn.IncubationCentre.Localization;
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

namespace IsikUn.IncubationCentre.Mentors
{
    [Authorize(IncubationCentrePermissions.Mentors.Default)]
    public class MentorAppService : ApplicationService, IMentorAppService
    {
        private readonly IMentorRepository _mentorRepository;
        private readonly IPersonSkillRepository _personSkillRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public MentorAppService(
            IMentorRepository mentorRepository, 
            IIdentityUserRepository identityUserRepository,
            IPersonSkillRepository personSkillRepository
            )
        {
            this._mentorRepository = mentorRepository;
            this._identityUserRepository = identityUserRepository;
            this._personSkillRepository = personSkillRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Mentors.Create)]
        public async Task<MentorDto> CreateAsync(CreateUpdateMentorDto input)
        {
            var mentor = ObjectMapper.Map<CreateUpdateMentorDto, Mentor>(input);
            mentor = await _mentorRepository.InsertAsync(mentor, autoSave: true);
            return ObjectMapper.Map<Mentor, MentorDto>(mentor);
        }

        [Authorize(IncubationCentrePermissions.Mentors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _personSkillRepository.DeleteManyAsync(await _personSkillRepository.GetListAsync(a => a.PersonId == id, true));
            await _mentorRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Mentors.Default)]
        public async Task<List<MentorDto>> GetAllItemsAsync()
        {
            var items = (await _mentorRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Mentor>, List<MentorDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Mentors.Default)]
        public async Task<MentorDto> GetAsync(Guid id)
        {
            var mentor = await _mentorRepository.GetAsync(id);
            return ObjectMapper.Map<Mentor, MentorDto>(mentor);
        }

        [Authorize(IncubationCentrePermissions.Mentors.Default)]
        public async Task<PagedResultDto<MentorDto>> GetListAsync(GetMentorsInput input)
        {
            long totalCount = 0;
            List<Mentor> items = null;
            if (input.IdentityUserId.HasValue)
            {
                var IdentityUser = await _identityUserRepository.GetAsync(input.IdentityUserId.Value);
                totalCount = await _mentorRepository.GetCountAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false);
                items = await _mentorRepository.GetListAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false, true, input.SkipCount, input.MaxResultCount, input.Sorting);
            }
            else
            {
                totalCount = await _mentorRepository.GetCountAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted:false);
                items = await _mentorRepository.GetListAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted: false, skipCount: input.SkipCount, maxResultCount: input.MaxResultCount, sorting: input.Sorting);
            }
            return new PagedResultDto<MentorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Mentor>, List<MentorDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Mentors.Edit)]
        public async Task<MentorDto> UpdateAsync(Guid id, CreateUpdateMentorDto input)
        {
            var mentor = await _mentorRepository.GetAsync(id);
            ObjectMapper.Map(input, mentor);
            mentor = await _mentorRepository.UpdateAsync(mentor, autoSave: true);
            mentor = await _mentorRepository.GetWithDetailAsync(id);
            return ObjectMapper.Map<Mentor, MentorDto>(mentor);

        }

        [Authorize(IncubationCentrePermissions.Mentors.Default)]
        public async Task<PagedResultDto<ProjectDto>> GetProjectListAsync(GetMentorsInput input)
        {
            var mentor = await _mentorRepository.GetWithDetailAsync(input.id.Value);
            List<Project> projects = mentor.MentoringProjects != null ? mentor.MentoringProjects.ToList() : null;
            return new PagedResultDto<ProjectDto>
            {
                TotalCount = mentor.MentoringProjects != null ? mentor.MentoringProjects.Count : 0,
                Items = ObjectMapper.Map<List<Project>, List<ProjectDto>>(projects)
            };
        }
    }
}
