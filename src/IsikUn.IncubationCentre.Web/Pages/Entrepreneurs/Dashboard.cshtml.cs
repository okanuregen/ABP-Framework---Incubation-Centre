using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.Tasks;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Entrepreneurs;
using Volo.Abp.Users;
using System.Linq;
using IsikUn.IncubationCentre.People;

namespace IsikUn.IncubationCentre.Web.Pages.Entrepreneurs
{
    public class DashboardModel : PageModel
    {
        public List<IsikUn.IncubationCentre.Tasks.Task> Tasks { get; set; }
        public List<Request> SentRequests { get; set; }
        public List<Request> ReceivedRequests { get; set; }
        public Project CurrentProject { get; set; }
        public Entrepreneur CurrentUser { get; set; }


        private readonly IEntrepreneurRepository _entreprenurRepo;
        private readonly IPersonRepository _personRepo;
        private readonly IProjectAppService _projectAppService;
        private readonly ITaskAppService _taskAppService;
        private readonly IRequestAppService _requestAppService;
        private readonly ICurrentUser _currentUser;

        public DashboardModel(
            IEntrepreneurRepository entreprenurRepo,
            IPersonRepository personRepo,
            IProjectAppService projectAppService,
            ITaskAppService taskAppService,
            IRequestAppService requestAppService,
            ICurrentUser currentUser
            )
        {
            this._entreprenurRepo = entreprenurRepo;
            this._personRepo = personRepo;
            this._projectAppService = projectAppService;
            this._taskAppService = taskAppService;
            this._requestAppService = requestAppService;
            this._currentUser = currentUser;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailAsync(_currentUser.GetId());
            CurrentUser = await _entreprenurRepo.GetWithDetailAsync(person.Id);
            SentRequests = person.SentRequests;
            ReceivedRequests = person.ReceivedRequests;
            Tasks = person.Tasks;
            CurrentProject = CurrentUser.MyProjects != null && CurrentUser.MyProjects.Any() ? CurrentUser.MyProjects.ToList().Where(a => a.Status == ProjectStatus.OnGoing).FirstOrDefault() : null;
        }
    }
}
