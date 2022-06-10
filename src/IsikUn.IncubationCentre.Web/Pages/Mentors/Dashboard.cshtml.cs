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

namespace IsikUn.IncubationCentre.Web.Pages.Mentors
{
    public class DashboardModel : PageModel
    {
        public List<Request> SentRequests { get; set; }
        public List<Request> ReceivedRequests { get; set; }


        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;

        public DashboardModel(
            IPersonRepository personRepo,
            ICurrentUser currentUser
            )
        {
            this._personRepo = personRepo;
            this._currentUser = currentUser;
        }
        public async Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            SentRequests = person.SentRequests;
            ReceivedRequests = person.ReceivedRequests;
        }
    }
}
