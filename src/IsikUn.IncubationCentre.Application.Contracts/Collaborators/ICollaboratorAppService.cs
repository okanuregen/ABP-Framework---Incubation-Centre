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
    }
}
