using IsikUn.IncubationCentre.Investors;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Projects
{
    public interface IProjectAppService : IApplicationService
    {
        Task<PagedResultDto<ProjectDto>> GetListAsync(GetProjectsInput input);

        Task<List<ProjectDto>> GetAllItemsAsync();

        Task<ProjectDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<ProjectDto> CreateAsync(CreateUpdateProjectDto input);

        Task<ProjectDto> UpdateAsync(Guid id, CreateUpdateProjectDto input);
        
        Task<ProjectDto> GetWithDetailAsync(Guid id);

        Task Invest(Guid projectId);

        Task<ProjectDto> AssignMentorAsync(Guid id, Guid mentorId);

        Task<PagedResultDto<ProjectDto>> GetListByInvestorAsync(GetInvestorsInput investor, string sorting = null, int skipCount = 0, int maxResultCount = int.MaxValue);
    }
}
