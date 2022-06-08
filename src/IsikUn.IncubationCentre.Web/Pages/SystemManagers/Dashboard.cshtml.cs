using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Entrepreneurs;
using Volo.Abp.Users;
using System.Linq;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.SystemManagers;
using IsikUn.IncubationCentre.Events;

namespace IsikUn.IncubationCentre.Web.Pages.SystemManagers
{
    public class DashboardModel : PageModel
    {
        public List<Request> SentRequests { get; set; }
        public List<Request> ReceivedRequests { get; set; }
        public SystemManager CurrentUser { get; set; }
        public long NumOfMembers { get; set; }
        public long NumOfEntreprenurs { get; set; }
        public long NumOfCollaborators { get; set; }
        public long NumOfMentors { get; set; }
        public long NumOfInvestors { get; set; }
        public long NumOfSystemManagers { get; set; }
        public long NumOfProjects { get; set; }
        public long NumOfProjectsInReview { get; set; }
        public long NumOfProjectsApproved { get; set; }
        public long NumOfProjectsDeclined { get; set; }
        public long NumOfProjectsStarted { get; set; }
        public long NumOfProjectsOnGoing { get; set; }
        public long NumOfProjectsSuspended { get; set; }
        public long NumOfProjectsCanceled { get; set; }
        public long NumOfProjectsCompleted { get; set; }
        public long NumOfEvents { get; set; }

        private readonly IPersonRepository _personRepo;
        private readonly IEntrepreneurRepository _entreprenurRepo;
        private readonly ICollaboratorRepository _collaboratorRepo;
        private readonly IMentorRepository _mentorRepo;
        private readonly IInvestorRepository _investorRepo;
        private readonly ISystemManagerRepository _smRepo;
        private readonly IProjectRepository _projectRepo;
        private readonly IEventRepository _eventRepo;
        private readonly IRequestAppService _requestAppService;
        private readonly ICurrentUser _currentUser;

        public DashboardModel(
            IPersonRepository personRepo,
            IEntrepreneurRepository entreprenurRepo,
            ICollaboratorRepository collaboratorRepo,
            IMentorRepository mentorRepo,
            IInvestorRepository investorRepo,
            ISystemManagerRepository smRepo,
            IProjectRepository projectRepo,
            IEventRepository eventRepo,
            IRequestAppService requestAppService,
            ICurrentUser currentUser
            )
        {
            this._personRepo = personRepo;
            this._entreprenurRepo = entreprenurRepo;
            this._collaboratorRepo = collaboratorRepo;
            this._mentorRepo = mentorRepo;
            this._investorRepo = investorRepo;
            this._smRepo = smRepo;
            this._projectRepo = projectRepo;
            this._eventRepo = eventRepo;
            this._requestAppService = requestAppService;
            this._currentUser = currentUser;

        }
        public async Task OnGetAsync()
        {
            NumOfEntreprenurs = await _entreprenurRepo.GetCountAsync();
            NumOfCollaborators = await _collaboratorRepo.GetCountAsync();
            NumOfMentors = await _mentorRepo.GetCountAsync();
            NumOfInvestors = await _investorRepo.GetCountAsync();
            NumOfSystemManagers = await _smRepo.GetCountAsync();
            NumOfProjects = await _projectRepo.GetCountAsync();
            NumOfEvents = await _eventRepo.GetCountAsync();
            NumOfMembers = await _personRepo.GetCountAsync();

            NumOfProjectsApproved = await _projectRepo.GetCountAsync(ProjectStatus.Approved,filterByStatus:true);
            NumOfProjectsDeclined = await _projectRepo.GetCountAsync(ProjectStatus.Declined,filterByStatus:true);
            NumOfProjectsStarted = await _projectRepo.GetCountAsync(ProjectStatus.Started,filterByStatus:true);
            NumOfProjectsOnGoing = await _projectRepo.GetCountAsync(ProjectStatus.OnGoing,filterByStatus:true);
            NumOfProjectsSuspended = await _projectRepo.GetCountAsync(ProjectStatus.Suspended,filterByStatus:true);
            NumOfProjectsCanceled = await _projectRepo.GetCountAsync(ProjectStatus.Canceled,filterByStatus:true);
            NumOfProjectsCompleted = await _projectRepo.GetCountAsync(ProjectStatus.Completed,filterByStatus:true);
            

            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            CurrentUser = await _smRepo.GetWithDetailAsync(person.Id);

            SentRequests = person.SentRequests;
            ReceivedRequests = person.ReceivedRequests;

        }
    }
}
