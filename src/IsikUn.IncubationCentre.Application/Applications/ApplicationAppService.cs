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

namespace IsikUn.IncubationCentre.Applications
{
    public class ApplicationAppService : ApplicationService, IApplicationAppService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationAppService(IApplicationRepository applicationRepository)
        {
            this._applicationRepository = applicationRepository;
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

            var application = ObjectMapper.Map<CreateUpdateApplicationDto, Application>(input);
            application = await _applicationRepository.InsertAsync(application, autoSave: true);
            return ObjectMapper.Map<Application, ApplicationDto>(application);
        }

        [Authorize(IncubationCentrePermissions.Applications.Delete)]
        public async Task DeleteAsync(Guid id)
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
