using IsikUn.IncubationCentre.Localization;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Permissions;
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

namespace IsikUn.IncubationCentre.SystemManagers
{
    [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
    public class SystemManagerAppService : ApplicationService, ISystemManagerAppService
    {
        private readonly ISystemManagerRepository _systemManagerRepository;
        private readonly IPersonSkillRepository _personSkillRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public SystemManagerAppService(
            ISystemManagerRepository systemManagerRepository, 
            IIdentityUserRepository identityUserRepository,
            IPersonSkillRepository personSkillRepository
            )
        {
            this._systemManagerRepository = systemManagerRepository;
            this._identityUserRepository = identityUserRepository;
            this._personSkillRepository = personSkillRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Create)]
        public async Task<SystemManagerDto> CreateAsync(CreateUpdateSystemManagerDto input)
        {
            var systemManager = ObjectMapper.Map<CreateUpdateSystemManagerDto, SystemManager>(input);
            systemManager = await _systemManagerRepository.InsertAsync(systemManager, autoSave: true);
            return ObjectMapper.Map<SystemManager, SystemManagerDto>(systemManager);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _personSkillRepository.DeleteManyAsync(await _personSkillRepository.GetListAsync(a => a.PersonId == id, true));
            await _systemManagerRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<List<SystemManagerDto>> GetAllItemsAsync()
        {
            var items = (await _systemManagerRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<SystemManager>, List<SystemManagerDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<SystemManagerDto> GetAsync(Guid id)
        {
            var systemManager = await _systemManagerRepository.GetAsync(id);
            return ObjectMapper.Map<SystemManager, SystemManagerDto>(systemManager);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<PagedResultDto<SystemManagerDto>> GetListAsync(GetSystemManagersInput input)
        {
            long totalCount = 0;
            List<SystemManager> items = null;
            if (input.IdentityUserId.HasValue)
            {
                var IdentityUser = await _identityUserRepository.GetAsync(input.IdentityUserId.Value);
                totalCount = await _systemManagerRepository.GetCountAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false);
                items = await _systemManagerRepository.GetListAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false, true, input.SkipCount, input.MaxResultCount, input.Sorting);
            }
            else
            {
                totalCount = await _systemManagerRepository.GetCountAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted:false);
                items = await _systemManagerRepository.GetListAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted: false, skipCount: input.SkipCount, maxResultCount: input.MaxResultCount, sorting: input.Sorting);
            }
            return new PagedResultDto<SystemManagerDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<SystemManager>, List<SystemManagerDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Edit)]
        public async Task<SystemManagerDto> UpdateAsync(Guid id, CreateUpdateSystemManagerDto input)
        {
            var systemManager = await _systemManagerRepository.GetAsync(id);
            ObjectMapper.Map(input, systemManager);
            systemManager = await _systemManagerRepository.UpdateAsync(systemManager, autoSave: true);
            systemManager = await _systemManagerRepository.GetWithDetailAsync(id);
            return ObjectMapper.Map<SystemManager, SystemManagerDto>(systemManager);

        }
    }
}
