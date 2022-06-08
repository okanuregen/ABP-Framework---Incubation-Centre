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
using IsikUn.IncubationCentre.SystemManagers;
using IsikUn.IncubationCentre.Tasks;
using Volo.Abp.Users;
using IsikUn.IncubationCentre.People;

namespace IsikUn.IncubationCentre.Applications
{
    public class ApplicationAppService : ApplicationService, IApplicationAppService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ISystemManagerRepository _systemManagerRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICurrentUser _currentUser;

        public ApplicationAppService(
            IApplicationRepository applicationRepository,
            ISystemManagerRepository systemManagerRepository,
            IPersonRepository personRepository,
            ICurrentUser currentUser,
            ITaskRepository taskRepository
            )
        {
            this._applicationRepository = applicationRepository;
            this._systemManagerRepository = systemManagerRepository;
            this._currentUser = currentUser;
            this._personRepository = personRepository;
            this._taskRepository = taskRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        [Authorize(IncubationCentrePermissions.Applications.Create)]
        public async Task<ApplicationDto> CreateAsync(CreateUpdateApplicationDto input)
        {
            var sameMailExist = await _applicationRepository.FindAsync(a => a.SenderMail == input.SenderMail);
            if (sameMailExist != null)
            {
                throw new UserFriendlyException(L["SameMailExistOnDifferentApplication"]);
            }

            var SystemManagers = await _systemManagerRepository.GetListAsync();
            var newTasks = SystemManagers.Select(a => new IsikUn.IncubationCentre.Tasks.Task
            {
                AssignedTo = a,
                AssignedToId = a.Id,
                isDone = false,
                Title = String.Format("New Application ({0} {1})", input.SenderName, input.SenderSurname),
                Description = String.Format("{0} {1} wants to join us as {2}. Look at the new applications", input.SenderName, input.SenderSurname, input.MembershipType.ToString())
            });
            await _taskRepository.InsertManyAsync(newTasks, true);

            input.ApplicationStatus = ApplicationStatus.InReview;
            var application = ObjectMapper.Map<CreateUpdateApplicationDto, Application>(input);
            application = await _applicationRepository.InsertAsync(application, autoSave: true);
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ApplicationDto> ApproveApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);
            if (application == null)
            {
                throw new UserFriendlyException(L["NoApplicationFound"]);
            }

            application.ApplicationStatus = ApplicationStatus.Approved;
            var systemManger = await _systemManagerRepository.GetAsync(a => a.IdentityUserId == _currentUser.Id.Value);
            if(systemManger != null)
            {
                application.ReceiverId = systemManger.Id;
            }

            //Send Inform Mail To User
            application = await _applicationRepository.UpdateAsync(application, autoSave: true);
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }

        [Authorize(IncubationCentrePermissions.SystemManagers.Default)]
        public async Task<ApplicationDto> RejectApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);
            if (application == null)
            {
                throw new UserFriendlyException(L["NoApplicationFound"]);
            }

            application.ApplicationStatus = ApplicationStatus.Declined;
            var systemManger = await _systemManagerRepository.GetAsync(a => a.IdentityUserId == _currentUser.Id.Value);
            if (systemManger != null)
            {
                application.ReceiverId = systemManger.Id;
            }

            //Send Inform Mail To User
            application = await _applicationRepository.UpdateAsync(application, autoSave: true);
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }

        [Authorize(IncubationCentrePermissions.Applications.Delete)]
        public async System.Threading.Tasks.Task DeleteAsync(Guid id)
        {
            await _applicationRepository.DeleteAsync(id, autoSave: true);
        }

        [Authorize(IncubationCentrePermissions.Applications.Default)]
        public async Task<List<ApplicationDto>> GetAllItemsAsync()
        {
            var items = (await _applicationRepository.GetQueryableAsync()).ToList();
            return ObjectMapper.Map<List<Application>, List<ApplicationDto>>(items);
        }

        [Authorize(IncubationCentrePermissions.Applications.Default)]
        public async Task<ApplicationDto> GetAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }

        [Authorize(IncubationCentrePermissions.Applications.Default)]
        public async Task<PagedResultDto<ApplicationDto>> GetListAsync(GetApplicationsInput input)
        {
            var totalCount = await _applicationRepository.GetCountAsync(input.filter, input.SenderName,input.SenderSurname,input.SenderMail,input.SenderPhoneNumber,input.Explanation,input.ApplicationStatus != null ? input.ApplicationStatus.ToString() : null);
            var items = await _applicationRepository.GetListAsync(input.filter, input.SenderName, input.SenderSurname, input.SenderMail, input.SenderPhoneNumber, input.Explanation, input.ApplicationStatus != null ? input.ApplicationStatus.ToString() : null, input.SkipCount,input.MaxResultCount,input.Sorting);

            return new PagedResultDto<ApplicationDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Application>, List<ApplicationDto>>(items)
            };
        }

        [Authorize(IncubationCentrePermissions.Applications.Edit)]
        public async Task<ApplicationDto> UpdateAsync(Guid id, CreateUpdateApplicationDto input)
        {
            var sameMailExist = await _applicationRepository.FindAsync(a => a.SenderMail == input.SenderMail && a.Id != id);
            if (sameMailExist != null)
            {
                throw new UserFriendlyException(L["SameMailExistOnDifferentApplication"]);
            }

            var application = await _applicationRepository.GetAsync(id);
            ObjectMapper.Map(input, application);
            application = await _applicationRepository.UpdateAsync(application, autoSave: true);
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }
    }
}
