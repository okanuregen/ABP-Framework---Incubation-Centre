using IsikUn.IncubationCentre.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Mentors
{
    public interface IMentorAppService : IApplicationService
    {
        Task<PagedResultDto<MentorDto>> GetListAsync(GetMentorsInput input);

        Task<List<MentorDto>> GetAllItemsAsync();

        Task<MentorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<MentorDto> CreateAsync(CreateUpdateMentorDto input);

        Task<MentorDto> UpdateAsync(Guid id, CreateUpdateMentorDto input);
        Task<PagedResultDto<ProjectDto>> GetProjectListAsync(GetMentorsInput input);
    }
}
