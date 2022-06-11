using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Investors;
using Volo.Abp.Users;
using System.Linq;
using System;

namespace IsikUn.IncubationCentre.Web.Pages.Investors
{
    public class DetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid id { get; set; }

        public bool IsMyPage = false;

        public Project CurrentProject { get; set; }
        public List<Project> Projects { get; set; }
        public Investor Investor { get; set; }


        private readonly IInvestorRepository _investorRepo;
        private readonly ICurrentUser _currentUser;

        public DetailModel(
            IInvestorRepository investorRepo,
            ICurrentUser currentUser
            )
        {
            this._currentUser = currentUser;
            this._investorRepo = investorRepo;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Investor = await _investorRepo.GetWithDetailAsync(id);
            IsMyPage = _currentUser.Id == Investor.IdentityUserId;
            Projects = Investor.InvestedProjects != null && Investor.InvestedProjects.Any() ? Investor.InvestedProjects.ToList() : null;
            // CurrentProject = Projects != null ? Projects.Where(a => a.Status ==  ProjectStatus.OnGoing).FirstOrDefault() : null;
        }
    }
}
