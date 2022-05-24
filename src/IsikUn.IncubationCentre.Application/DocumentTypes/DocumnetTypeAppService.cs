//using IsikUn.IncubationCentre.Localization;
//using IsikUn.IncubationCentre.PeopleSkills;
//using IsikUn.IncubationCentre.Permissions;
//using Microsoft.AspNetCore.Authorization;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Application.Services;
//using Volo.Abp.Domain.Repositories;

//namespace IsikUn.IncubationCentre.Skills
//{
//    [Authorize(IncubationCentrePermissions.Skills.Default)]
//    public class SkillAppService : ApplicationService, ISkillAppService
//    {
//        private readonly ISkillRepository _skillRepository;
//        private readonly IPersonSkillRepository _personSkillRepository;

//        public SkillAppService(ISkillRepository skillRepository, IPersonSkillRepository personSkillRepository)
//        {
//            this._skillRepository = skillRepository;
//            this._personSkillRepository = personSkillRepository;
//            LocalizationResource = typeof(IncubationCentreResource);
//        }

//        [Authorize(IncubationCentrePermissions.Skills.Create)]
//        public async Task<SkillDto> CreateAsync(CreateSkillDto input)
//        {
//            var skillNameAndCategoryExist = await _skillRepository.AnyAsync(a => a.Name == input.Name && a.Category == input.Category);
//            if (skillNameAndCategoryExist)
//            {
//                throw new UserFriendlyException(L["SkillNameOnSameCategoryExist"]);
//            }

//            var skill = ObjectMapper.Map<CreateSkillDto, Skill>(input);
//            skill = await _skillRepository.InsertAsync(skill, autoSave: true);
//            return ObjectMapper.Map<Skill, SkillDto>(skill);
//        }

//        [Authorize(IncubationCentrePermissions.Skills.Delete)]
//        public async Task DeleteAsync(Guid id)
//        {
//            await _personSkillRepository.DeleteManyAsync(await _personSkillRepository.GetListAsync(a => a.SkillId == id, true));
//            await _skillRepository.DeleteAsync(id, autoSave:true);
//        }

//        [Authorize(IncubationCentrePermissions.Skills.Default)]
//        public async Task<List<SkillDto>> GetAllItemsAsync()
//        {
//            var items =  (await _skillRepository.GetQueryableAsync()).ToList();
//            return ObjectMapper.Map<List<Skill>, List<SkillDto>>(items);
//        }

//        [Authorize(IncubationCentrePermissions.Skills.Default)]
//        public async Task<SkillDto> GetAsync(Guid id)
//        {
//            var skill = await _skillRepository.GetAsync(id);
//            return ObjectMapper.Map<Skill, SkillDto>(skill);
//        }

//        [Authorize(IncubationCentrePermissions.Skills.Default)]
//        public async Task<PagedResultDto<SkillDto>> GetListAsync(GetSkillsInput input)
//        {
//            var totalCount = await _skillRepository.GetCountAsync(input.filter, input.Name, input.Category);
//            var items = await _skillRepository.GetListAsync(input.filter, input.Name, input.Category, input.SkipCount, input.MaxResultCount, input.Sorting);

//            return new PagedResultDto<SkillDto>
//            {
//                TotalCount = totalCount,
//                Items = ObjectMapper.Map<List<Skill>, List<SkillDto>>(items)
//            };
//        }

//        [Authorize(IncubationCentrePermissions.Skills.Edit)]
//        public async Task<SkillDto> UpdateAsync(Guid id, UpdateSkillDto input)
//        {
//            var skillNameAndCategoryExist = await _skillRepository.AnyAsync(a => a.Name == input.Name && a.Category == input.Category && a.Id != id);
//            if (skillNameAndCategoryExist)
//            {
//                throw new UserFriendlyException(L["SkillNameOnSameCategoryExist"]);
//            }

//            var skill = await _skillRepository.GetAsync(id);
//            ObjectMapper.Map(input, skill);
//            skill = await _skillRepository.UpdateAsync(skill, autoSave: true);
//            return ObjectMapper.Map<Skill, SkillDto>(skill);
//        }
//    }
//}
