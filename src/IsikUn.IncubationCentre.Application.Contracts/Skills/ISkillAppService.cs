using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Skills
{
    public interface ISkillAppService : IApplicationService
    {
        Task<PagedResultDto<SkillDto>> GetListAsync(GetSkillsInput input);

        Task<SkillDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<SkillDto> CreateAsync(CreateSkillDto input);

        Task<SkillDto> UpdateAsync(Guid id, UpdateSkillDto input);
    }
}
