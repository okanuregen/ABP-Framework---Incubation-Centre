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
using IsikUn.IncubationCentre.Projects;
using Volo.Abp.Users;
using IsikUn.IncubationCentre.People;

namespace IsikUn.IncubationCentre.Requests
{
    public class RequestAppService : ApplicationService, IRequestAppService
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICurrentUser _currentUser;

        public RequestAppService(IRequestRepository requestRepository, IProjectRepository projectRepository, ICurrentUser currentUser, IPersonRepository personRepository)
        {
            this._requestRepository = requestRepository;
            this._projectRepository = projectRepository;
            this._personRepository = personRepository;
            this._currentUser = currentUser;
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
            var totalCount = await _requestRepository.GetCountAsync(input.filter, input.Title, input.Explanation, input.SenderUserName, input.ReceiverUserName);
            var items = await _requestRepository.GetListAsync(input.filter, input.Title, input.Explanation, input.SenderUserName, input.ReceiverUserName, input.SkipCount, input.MaxResultCount, input.Sorting);

            return new PagedResultDto<RequestDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Request>, List<RequestDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task SendFeedbackToProjectOwnersAsync(Guid projectId, bool isApproved = false, string feedback = null)
        {
            var project = await _projectRepository.GetWithDetailAsync(projectId);
            var sender = await _personRepository.GetWithDetailByIdentityUserIdAsync(_currentUser.Id.Value);
            List<Request> requestes = new List<Request>();
            if (project.Entrepreneurs != null && project.Entrepreneurs.Count() > 0)
            {
                project.Entrepreneurs.ToList().ForEach(a => requestes.Add(new Request
                {
                    SenderId = sender.Id,
                    Title = isApproved ? L["ProjectApproved"] : L["ProjectRejected"],
                    Explanation = feedback != null ? feedback : "",
                    ReceiverId = a.Id,
                    Sender = sender,
                    Receiver = a
                }));
            }

            if (project.Collaborators != null && project.Collaborators.Count() > 0)
            {
                project.Collaborators.ToList().ForEach(a => requestes.Add(new Request
                {
                    SenderId = sender.Id,
                    Title = isApproved ? L["ProjectApproved"] : L["ProjectRejected"],
                    Explanation = feedback != null ? feedback : "",
                    ReceiverId = a.Id,
                    Sender = sender,
                    Receiver = a
                }));
            }
            if (requestes.Any())
            {
                await _requestRepository.InsertManyAsync(requestes, true);
            }

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
