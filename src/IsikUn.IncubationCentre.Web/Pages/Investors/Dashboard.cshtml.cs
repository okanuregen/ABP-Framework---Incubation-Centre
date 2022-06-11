using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Projects;
using Volo.Abp.Users;
using System.Linq;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Investors;

namespace IsikUn.IncubationCentre.Web.Pages.Investors
{
    public class DashboardModel : PageModel
    {
        public List<Request> SentRequests { get; set; }
        public List<Request> ReceivedRequests { get; set; }
        public List<Project> Projects { get; set; }
        public Investor CurrentUser { get; set; }



        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IInvestorRepository _investorRepo;
        private readonly IProjectRepository _projectRepo;


        public DashboardModel(
            IPersonRepository personRepo,
            ICurrentUser currentUser,
            IProjectRepository projectRepo,
            IInvestorRepository investorRepo
            )
        {
            this._personRepo = personRepo;
            this._currentUser = currentUser;
            this._investorRepo = investorRepo;
            this._projectRepo = projectRepo;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            CurrentUser = await _investorRepo.GetWithDetailAsync(person.Id);
            SentRequests = person.SentRequests;
            ReceivedRequests = person.ReceivedRequests;
            // projects that the investor already invested in.
            Projects = CurrentUser.InvestedProjects != null && CurrentUser.InvestedProjects.Any() ? CurrentUser.InvestedProjects.ToList() : null;

        }
    }
}
