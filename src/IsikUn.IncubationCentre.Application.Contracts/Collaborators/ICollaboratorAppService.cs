using IsikUn.IncubationCentre.Milestones;
using IsikUn.IncubationCentre.Projects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Collaborators
{
    public interface ICollaboratorAppService : IApplicationService
    {
        Task<PagedResultDto<CollaboratorDto>> GetListAsync(GetCollaboratorsInput input);

        Task<List<CollaboratorDto>> GetAllItemsAsync();

        Task<CollaboratorDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<CollaboratorDto> CreateAsync(CreateUpdateCollaboratorDto input);

        Task<CollaboratorDto> UpdateAsync(Guid id, CreateUpdateCollaboratorDto input);
        Task<PagedResultDto<ProjectDto>> GetProjectListAsync(GetCollaboratorsInput input);
        Task<PagedResultDto<MilestoneDto>> GetMilestoneListAsync(GetCollaboratorsInput input);
    }
}
