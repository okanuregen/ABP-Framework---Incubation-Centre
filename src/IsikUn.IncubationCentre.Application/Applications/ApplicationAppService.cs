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
using Volo.Abp.Emailing;
using Volo.Abp.Identity;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Entrepreneurs;
using System.Text;
using System.Globalization;

namespace IsikUn.IncubationCentre.Applications
{
    public class ApplicationAppService : ApplicationService, IApplicationAppService
    {
        private readonly IIdentityUserAppService _identityUserAppService;
        private readonly IIdentityUserRepository _identityUserRepo;
        private readonly IInvestorAppService _investorAppService;
        private readonly IMentorAppService _mentorAppService;
        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly IEntrepreneurAppService _entreprenurAppService;
        private readonly IApplicationRepository _applicationRepository;
        private readonly ISystemManagerRepository _systemManagerRepository;
        private readonly ITaskRepository _taskRepository;
        private readonly IPersonRepository _personRepository;
        private readonly ICurrentUser _currentUser;

        public ApplicationAppService(
            IInvestorAppService investorAppService,
            IIdentityUserRepository identityUserRepo,
            IMentorAppService mentorAppService,
            ICollaboratorAppService collaboratorAppService,
            IEntrepreneurAppService entreprenurAppService,
            IIdentityUserAppService identityUserAppService,
            IApplicationRepository applicationRepository,
            ISystemManagerRepository systemManagerRepository,
            IPersonRepository personRepository,
            ICurrentUser currentUser,
            ITaskRepository taskRepository
            )
        {
            this._identityUserRepo = identityUserRepo;
            this._investorAppService = investorAppService;
            this._mentorAppService = mentorAppService;
            this._collaboratorAppService = collaboratorAppService;
            this._entreprenurAppService = entreprenurAppService;
            this._identityUserAppService = identityUserAppService;
            this._applicationRepository = applicationRepository;
            this._systemManagerRepository = systemManagerRepository;
            this._currentUser = currentUser;
            this._personRepository = personRepository;
            this._taskRepository = taskRepository;
            LocalizationResource = typeof(IncubationCentreResource);
        }

        public async Task<ApplicationDto> CreateAsync(CreateUpdateApplicationDto input)
        {
            var sameMailExist = await _applicationRepository.FindAsync(a => a.SenderMail == input.SenderMail && a.ApplicationStatus != ApplicationStatus.Declined);
            if(sameMailExist != null)
            {
                throw new UserFriendlyException(L["SameMailExistOnDifferentApplication"]);
            }
            else
            {
                var users = await _identityUserRepo.GetListAsync(); 
                if(users.Count(a => a.Email == input.SenderMail) > 0)
                {
                    throw new UserFriendlyException(L["SameMailExistOnDifferentApplication"]);
                }
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
        public async Task<PersonDto> ApproveApplicationAsync(Guid id)
        {
            var application = await _applicationRepository.GetAsync(id);
            if (application == null)
            {
                throw new UserFriendlyException(L["NoApplicationFound"]);
            }

            application.ApplicationStatus = ApplicationStatus.Approved;
            application = await _applicationRepository.UpdateAsync(application, autoSave: true);

            //create identityUser
            string[] roles = new string[1];
            roles[0] = application.MembershipType.ToString();

            var users = await _identityUserAppService.GetListAsync(new GetIdentityUsersInput());
            
            var userName = String.Format("{0}.{1}", application.SenderName.ToLower().Replace(" ", ""), application.SenderSurname.ToLower().Replace(" ", ""));
            userName = String.Join("", userName.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ý", "i");
            
            var userNameExist = users.Items.Count(a => a.UserName == userName) > 0;
            
            while (userNameExist)
            {
                userName = String.Format("{0}.{1}{2}", application.SenderName.ToLower().Replace(" ", ""), application.SenderSurname.ToLower().Replace(" ", ""), new Random().NextInt64(10, 99));
                userName = String.Join("", userName.Normalize(NormalizationForm.FormD).Where(c => char.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)).Replace("ý", "i");
                userNameExist = users.Items.Count(a => a.UserName == userName) > 0;
            }

            var identityUser = new IdentityUserCreateDto
            {
                Email = application.SenderMail,
                UserName = userName,
                IsActive = true,
                LockoutEnabled = false,
                Name = application.SenderName,
                Surname = application.SenderSurname,
                Password = "123456Aa.",
                RoleNames = roles
            };
            var newUser = await _identityUserAppService.CreateAsync(identityUser);

            PersonDto person = null;
            //our user type
            switch (application.MembershipType)
            {
                case ApplicationType.Investor:
                    person = await _investorAppService.CreateAsync(new CreateUpdateInvestorDto { CreationTime = DateTime.Now, IdentityUserId =newUser.Id,isActivated = true, About = String.Format("{0} {1}", application.SenderName, application.SenderSurname) });
                    break;
                case ApplicationType.Mentor:
                    person = await _mentorAppService.CreateAsync(new CreateUpdateMentorDto { CreationTime = DateTime.Now, IdentityUserId = newUser.Id, isActivated = true, About = String.Format("{0} {1}", application.SenderName, application.SenderSurname) });
                    break;
                case ApplicationType.Collaborator:
                    person = await _collaboratorAppService.CreateAsync(new CreateUpdateCollaboratorDto { CreationTime = DateTime.Now,IdentityUserId = newUser.Id, isActivated = true, About = String.Format("{0} {1}", application.SenderName, application.SenderSurname) });
                    break;
                case ApplicationType.Entrepreneur:
                    person = await _entreprenurAppService.CreateAsync(new CreateUpdateEntrepreneurDto { CreationTime = DateTime.Now,IdentityUserId = newUser.Id, isActivated = true,About = String.Format("{0} {1}", application.SenderName, application.SenderSurname) });
                    break;
            }
            if(person != null)
            {
                person.IdentityUser = newUser;
            }

            //Send Inform Mail To User
            return person;
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
            var totalCount = await _applicationRepository.GetCountAsync(input.filter, input.SenderName, input.SenderSurname, input.SenderMail, input.SenderPhoneNumber, input.Explanation, input.ApplicationStatus != null ? input.ApplicationStatus.ToString() : null);
            var items = await _applicationRepository.GetListAsync(input.filter, input.SenderName, input.SenderSurname, input.SenderMail, input.SenderPhoneNumber, input.Explanation, input.ApplicationStatus != null ? input.ApplicationStatus.ToString() : null, input.SkipCount, input.MaxResultCount, input.Sorting);

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
