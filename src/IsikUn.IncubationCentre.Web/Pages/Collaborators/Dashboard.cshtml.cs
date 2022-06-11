using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.Tasks;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Collaborators;
using Volo.Abp.Users;
using System.Linq;
using IsikUn.IncubationCentre.People;

namespace IsikUn.IncubationCentre.Web.Pages.Collaborators
{
    public class DashboardModel : PageModel
    {
        public List<IsikUn.IncubationCentre.Tasks.Task> Tasks { get; set; }
        public List<Request> SentRequests { get; set; }
        public List<Request> ReceivedRequests { get; set; }
        public Project CurrentProject { get; set; }
        public Collaborator CurrentUser { get; set; }


        private readonly ICollaboratorRepository _collaboratorRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IProjectAppService _projectAppService;
        private readonly ITaskAppService _taskAppService;
        private readonly IRequestAppService _requestAppService;
        private readonly ICurrentUser _currentUser;

        public DashboardModel(
            ICollaboratorRepository collaboratorRepo,
            IPersonRepository personRepo,
            IProjectAppService projectAppService,
            ITaskAppService taskAppService,
            IRequestAppService requestAppService,
            ICurrentUser currentUser
            )
        {
            this._collaboratorRepo = collaboratorRepo;
            this._personRepo = personRepo;
            this._projectAppService = projectAppService;
            this._taskAppService = taskAppService;
            this._requestAppService = requestAppService;
            this._currentUser = currentUser;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            CurrentUser = await _collaboratorRepo.GetWithDetailAsync(person.Id);
            SentRequests = person.SentRequests;
            ReceivedRequests = person.ReceivedRequests;
            Tasks = person.Tasks;
            CurrentProject = CurrentUser.CollaboratoringProjects != null && CurrentUser.CollaboratoringProjects.Any() ? CurrentUser.CollaboratoringProjects.ToList().Where(a => a.Status == ProjectStatus.OnGoing).FirstOrDefault() : null;
        }
    }
}
