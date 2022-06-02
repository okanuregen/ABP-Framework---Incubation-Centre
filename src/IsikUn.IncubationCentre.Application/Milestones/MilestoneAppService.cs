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

namespace IsikUn.IncubationCentre.Milestones
{
    public class MilestoneAppService : ApplicationService, IMilestoneAppService
    {
        private readonly IMilestoneRepository _milestoneRepository;

        public MilestoneAppService(IMilestoneRepository milestoneRepository)
        {
            this._milestoneRepository = milestoneRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Milestones.Create)]
        public async Task<MilestoneDto> CreateAsync(CreateUpdateMilestoneDto input)
        {
            var sameNameExist = await _milestoneRepository.FindAsync(a => a.Title == input.Title && a.ProjectId == input.ProjectId);
            if (sameNameExist != null)
            {
                throw new UserFriendlyException(L["MilestoneWithThisTitleOnSameProjectExist"]);
            }

            var milestone = ObjectMapper.Map<CreateUpdateMilestoneDto, Milestone>(input);
            milestone = await _milestoneRepository.InsertAsync(milestone, autoSave: true);
            return ObjectMapper.Map<Milestone, MilestoneDto>(milestone);
        }

        [Authorize(IncubationCentrePermissions.Milestones.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _milestoneRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Milestones.Default)]
        public async Task<List<MilestoneDto>> GetAllItemsAsync()
        {
            var items = (await _milestoneRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Milestone>, List<MilestoneDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Milestones.Default)]
        public async Task<MilestoneDto> GetAsync(Guid id)
        {
            var milestone = await _milestoneRepository.GetAsync(id);
            return ObjectMapper.Map<Milestone, MilestoneDto>(milestone);
        }

        [Authorize(IncubationCentrePermissions.Milestones.Default)]
        public async Task<PagedResultDto<MilestoneDto>> GetListAsync(GetMilestonesInput input)
        {
            var totalCount = await _milestoneRepository.GetCountAsync(input.filter, input.Title,input.SuccessCriteria,false,input.isCompleted,input.ProjectId.ToString());
            var items = await _milestoneRepository.GetListAsync(input.filter, input.Title,input.SuccessCriteria,false,input.isCompleted,input.ProjectId.ToString(), input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<MilestoneDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Milestone>, List<MilestoneDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Milestones.Edit)]
        public async Task<MilestoneDto> UpdateAsync(Guid id, CreateUpdateMilestoneDto input)
        {
            var sameNameExist = await _milestoneRepository.FindAsync(a => a.Title == input.Title && a.ProjectId == input.ProjectId && a.Id != id);
            if (sameNameExist != null)
            {
                throw new UserFriendlyException(L["MilestoneWithThisTitleOnSameProjectExist"]);
            }

            var milestone = await _milestoneRepository.GetAsync(id);
            ObjectMapper.Map(input, milestone);
            milestone = await _milestoneRepository.UpdateAsync(milestone, autoSave: true);
            return ObjectMapper.Map<Milestone, MilestoneDto>(milestone);
        }
    }
}
