using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace IsikUn.IncubationCentre.Documents
{
    public interface IDocumentAppService : IApplicationService
    {
        Task<PagedResultDto<DocumentDto>> GetListAsync(GetDocumentsInput input);

        Task<List<DocumentDto>> GetAllItemsAsync();

        Task<DocumentDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<DocumentDto> CreateAsync(CreateUpdateDocumentDto input);

        Task<DocumentDto> UpdateAsync(Guid id, CreateUpdateDocumentDto input);
    }
}