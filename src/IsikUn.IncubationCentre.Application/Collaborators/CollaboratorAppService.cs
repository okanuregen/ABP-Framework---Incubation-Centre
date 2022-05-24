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

namespace IsikUn.IncubationCentre.Collaborators
{
    [Authorize(IncubationCentrePermissions.Collaborators.Default)]
    public class CollaboratorAppService : ApplicationService, ICollaboratorAppService
    {
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly IPersonSkillRepository _personSkillRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public CollaboratorAppService(
            ICollaboratorRepository collaboratorRepository, 
            IIdentityUserRepository identityUserRepository,
            IPersonSkillRepository personSkillRepository
            )
        {
            this._collaboratorRepository = collaboratorRepository;
            this._identityUserRepository = identityUserRepository;
            this._personSkillRepository = personSkillRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Collaborators.Create)]
        public async Task<CollaboratorDto> CreateAsync(CreateUpdateCollaboratorDto input)
        {
            var collaborator = ObjectMapper.Map<CreateUpdateCollaboratorDto, Collaborator>(input);
            collaborator = await _collaboratorRepository.InsertAsync(collaborator, autoSave: true);
            return ObjectMapper.Map<Collaborator, CollaboratorDto>(collaborator);
        }

        [Authorize(IncubationCentrePermissions.Collaborators.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _personSkillRepository.DeleteManyAsync(await _personSkillRepository.GetListAsync(a => a.PersonId == id, true));
            await _collaboratorRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Collaborators.Default)]
        public async Task<List<CollaboratorDto>> GetAllItemsAsync()
        {
            var items = (await _collaboratorRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Collaborator>, List<CollaboratorDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Collaborators.Default)]
        public async Task<CollaboratorDto> GetAsync(Guid id)
        {
            var collaborator = await _collaboratorRepository.GetAsync(id);
            return ObjectMapper.Map<Collaborator, CollaboratorDto>(collaborator);
        }

        [Authorize(IncubationCentrePermissions.Collaborators.Default)]
        public async Task<PagedResultDto<CollaboratorDto>> GetListAsync(GetCollaboratorsInput input)
        {
            long totalCount = 0;
            List<Collaborator> items = null;
            if (input.IdentityUserId.HasValue)
            {
                var IdentityUser = await _identityUserRepository.GetAsync(input.IdentityUserId.Value);
                totalCount = await _collaboratorRepository.GetCountAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false);
                items = await _collaboratorRepository.GetListAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false, true, input.SkipCount, input.MaxResultCount, input.Sorting);
            }
            else
            {
                totalCount = await _collaboratorRepository.GetCountAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted:false);
                items = await _collaboratorRepository.GetListAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted: false, skipCount: input.SkipCount, maxResultCount: input.MaxResultCount, sorting: input.Sorting);
            }
            return new PagedResultDto<CollaboratorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Collaborator>, List<CollaboratorDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Collaborators.Edit)]
        public async Task<CollaboratorDto> UpdateAsync(Guid id, CreateUpdateCollaboratorDto input)
        {
            var collaborator = await _collaboratorRepository.GetAsync(id);
            ObjectMapper.Map(input, collaborator);
            collaborator = await _collaboratorRepository.UpdateAsync(collaborator, autoSave: true);
            collaborator = await _collaboratorRepository.GetWithDetailAsync(id);
            return ObjectMapper.Map<Collaborator, CollaboratorDto>(collaborator);

        }
    }
}
