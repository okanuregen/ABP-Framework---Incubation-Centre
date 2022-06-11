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
using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Mentors
{
    public class DashboardModel : PageModel
    {
        public List<Request> SentRequests { get; set; }
        public List<Request> ReceivedRequests { get; set; }
        public List<IsikUn.IncubationCentre.Tasks.Task> Tasks { get; set; }
        public List<Project> Projects { get; set; }
        public Mentor CurrentUser  { get; set; }  



        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IMentorRepository _mentorRepo;
        private readonly ITaskAppService _taskAppService;
        private readonly IProjectRepository _projectRepo;


        public DashboardModel(
            IPersonRepository personRepo,
            ICurrentUser currentUser,
            ITaskAppService taskAppService,
            IProjectRepository projectRepo,
            IMentorRepository mentorRepo
            )
        {
            this._personRepo = personRepo;
            this._mentorRepo = mentorRepo;
            this._currentUser = currentUser;
            this._taskAppService = taskAppService;
            this._projectRepo = projectRepo;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            CurrentUser = await _mentorRepo.GetWithDetailAsync(person.Id);
            SentRequests = person.SentRequests;
            ReceivedRequests = person.ReceivedRequests;
            Tasks = person.Tasks;
            Projects = CurrentUser.MentoringProjects != null && CurrentUser.MentoringProjects.Any() ? CurrentUser.MentoringProjects.ToList() : null;
            
        }
    }
}
