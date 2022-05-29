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

namespace IsikUn.IncubationCentre.Documents
{
    public class DocumentAppService : ApplicationService, IDocumentAppService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentAppService(IDocumentRepository documentRepository)
        {
            this._documentRepository = documentRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Documents.Create)]
        public async Task<DocumentDto> CreateAsync(CreateUpdateDocumentDto input)
        {
            var sameNameExist = await _documentRepository.FindAsync(a => a.FullName == input.FullName);
            if (sameNameExist != null)
            {
                throw new UserFriendlyException(L["SameNameFileExist"]);
            }

            var document = ObjectMapper.Map<CreateUpdateDocumentDto, Document>(input);
            document = await _documentRepository.InsertAsync(document, autoSave: true);
            return ObjectMapper.Map<Document, DocumentDto>(document);
        }

        [Authorize(IncubationCentrePermissions.Documents.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _documentRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Documents.Default)]
        public async Task<List<DocumentDto>> GetAllItemsAsync()
        {
            var items = (await _documentRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Document>, List<DocumentDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Documents.Default)]
        public async Task<DocumentDto> GetAsync(Guid id)
        {
            var document = await _documentRepository.GetAsync(id);
            return ObjectMapper.Map<Document, DocumentDto>(document);
        }

        [Authorize(IncubationCentrePermissions.Documents.Default)]
        public async Task<PagedResultDto<DocumentDto>> GetListAsync(GetDocumentsInput input)
        {
            var totalCount = await _documentRepository.GetCountAsync(input.filter, input.Name);
            var items = await _documentRepository.GetListAsync(input.filter, input.Name, input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<DocumentDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Document>, List<DocumentDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Documents.Edit)]
        public async Task<DocumentDto> UpdateAsync(Guid id, CreateUpdateDocumentDto input)
        {
            var sameNameExist = await _documentRepository.FindAsync(a => a.FullName == input.FullName && a.Id != id);
            if (sameNameExist != null)
            {
                throw new UserFriendlyException(L["SameNameFileExist"]);
            }

            var document = await _documentRepository.GetAsync(id);
            ObjectMapper.Map(input, document);
            document = await _documentRepository.UpdateAsync(document, autoSave: true);
            return ObjectMapper.Map<Document, DocumentDto>(document);
        }
    }
}
