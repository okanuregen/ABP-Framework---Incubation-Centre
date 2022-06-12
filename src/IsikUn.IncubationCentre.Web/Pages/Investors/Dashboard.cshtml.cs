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
        public Investor CurrentUser { get; set; }


        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IInvestorRepository _investorRepo;


        public DashboardModel(
            IPersonRepository personRepo,
            ICurrentUser currentUser,
            IInvestorRepository investorRepo
            )
        {
            this._personRepo = personRepo;
            this._currentUser = currentUser;
            this._investorRepo = investorRepo;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            CurrentUser = await _investorRepo.GetWithDetailAsync(person.Id);
            SentRequests = person.SentRequests != null && person.SentRequests.Any() ? person.SentRequests.OrderByDescending(a => a.CreationTime).ToList() : person.SentRequests;
            ReceivedRequests = person.ReceivedRequests != null && person.ReceivedRequests.Any() ? person.ReceivedRequests.OrderByDescending(a => a.CreationTime).ToList() : person.ReceivedRequests;
        }
    }
}
