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

namespace IsikUn.IncubationCentre.Requests
{
    public class RequestAppService : ApplicationService, IRequestAppService
    {
        private readonly IRequestRepository _requestRepository;

        public RequestAppService(IRequestRepository requestRepository)
        {
            this._requestRepository = requestRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Requests.Create)]
        public async Task<RequestDto> CreateAsync(CreateUpdateRequestDto input)
        {
            var Request = ObjectMapper.Map<CreateUpdateRequestDto, Request>(input);
            Request = await _requestRepository.InsertAsync(Request, autoSave: true);
            return ObjectMapper.Map<Request, RequestDto>(Request);
        }

        [Authorize(IncubationCentrePermissions.Requests.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _requestRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Requests.Default)]
        public async Task<List<RequestDto>> GetAllItemsAsync()
        {
            var items = (await _requestRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Request>, List<RequestDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Requests.Default)]
        public async Task<RequestDto> GetAsync(Guid id)
        {
            var Request = await _requestRepository.GetAsync(id);
            return ObjectMapper.Map<Request, RequestDto>(Request);
        }

        [Authorize(IncubationCentrePermissions.Requests.Default)]
        public async Task<PagedResultDto<RequestDto>> GetListAsync(GetRequestsInput input)
        {
            var totalCount = await _requestRepository.GetCountAsync(input.filter, input.Title,input.Explanation,input.SenderUserName,input.ReceiverUserName);
            var items = await _requestRepository.GetListAsync(input.filter, input.Title, input.Explanation, input.SenderUserName, input.ReceiverUserName, input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<RequestDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Request>, List<RequestDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Requests.Edit)]
        public async Task<RequestDto> UpdateAsync(Guid id, CreateUpdateRequestDto input)
        {
            var Request = await _requestRepository.GetAsync(id);
            ObjectMapper.Map(input, Request);
            Request = await _requestRepository.UpdateAsync(Request, autoSave: true);
            return ObjectMapper.Map<Request, RequestDto>(Request);
        }
    }
}
