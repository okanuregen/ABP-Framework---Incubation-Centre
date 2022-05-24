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

namespace IsikUn.IncubationCentre.Investors
{
    [Authorize(IncubationCentrePermissions.Investors.Default)]
    public class InvestorAppService : ApplicationService, IInvestorAppService
    {
        private readonly IInvestorRepository _investorRepository;
        private readonly IPersonSkillRepository _personSkillRepository;
        private readonly IIdentityUserRepository _identityUserRepository;

        public InvestorAppService(
            IInvestorRepository investorRepository, 
            IIdentityUserRepository identityUserRepository,
            IPersonSkillRepository personSkillRepository
            )
        {
            this._investorRepository = investorRepository;
            this._identityUserRepository = identityUserRepository;
            this._personSkillRepository = personSkillRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Investors.Create)]
        public async Task<InvestorDto> CreateAsync(CreateUpdateInvestorDto input)
        {
            var investor = ObjectMapper.Map<CreateUpdateInvestorDto, Investor>(input);
            investor = await _investorRepository.InsertAsync(investor, autoSave: true);
            return ObjectMapper.Map<Investor, InvestorDto>(investor);
        }

        [Authorize(IncubationCentrePermissions.Investors.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _personSkillRepository.DeleteManyAsync(await _personSkillRepository.GetListAsync(a => a.PersonId == id, true));
            await _investorRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Investors.Default)]
        public async Task<List<InvestorDto>> GetAllItemsAsync()
        {
            var items = (await _investorRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Investor>, List<InvestorDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Investors.Default)]
        public async Task<InvestorDto> GetAsync(Guid id)
        {
            var investor = await _investorRepository.GetAsync(id);
            return ObjectMapper.Map<Investor, InvestorDto>(investor);
        }

        [Authorize(IncubationCentrePermissions.Investors.Default)]
        public async Task<PagedResultDto<InvestorDto>> GetListAsync(GetInvestorsInput input)
        {
            long totalCount = 0;
            List<Investor> items = null;
            if (input.IdentityUserId.HasValue)
            {
                var IdentityUser = await _identityUserRepository.GetAsync(input.IdentityUserId.Value);
                totalCount = await _investorRepository.GetCountAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false);
                items = await _investorRepository.GetListAsync(input.filter, IdentityUser.UserName, IdentityUser.Name, IdentityUser.Surname, IdentityUser.Email, IdentityUser.PhoneNumber, input.Experience, input.SkillIds, input.About, false, true, input.SkipCount, input.MaxResultCount, input.Sorting);
            }
            else
            {
                totalCount = await _investorRepository.GetCountAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted:false);
                items = await _investorRepository.GetListAsync(filter: input.filter, experience: input.Experience, SkillIds: input.SkillIds, about: input.About, filterByActiveted: false, skipCount: input.SkipCount, maxResultCount: input.MaxResultCount, sorting: input.Sorting);
            }
            return new PagedResultDto<InvestorDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Investor>, List<InvestorDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Investors.Edit)]
        public async Task<InvestorDto> UpdateAsync(Guid id, CreateUpdateInvestorDto input)
        {
            var investor = await _investorRepository.GetAsync(id);
            ObjectMapper.Map(input, investor);
            investor = await _investorRepository.UpdateAsync(investor, autoSave: true);
            investor = await _investorRepository.GetWithDetailAsync(id);
            return ObjectMapper.Map<Investor, InvestorDto>(investor);

        }
    }
}
