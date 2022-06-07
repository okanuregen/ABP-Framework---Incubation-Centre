using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Entrepreneurs;
using Volo.Abp.Users;
using System.Linq;
using System;

namespace IsikUn.IncubationCentre.Web.Pages.Entrepreneurs
{
    public class DetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid id { get; set; }

        public bool IsMyPage = false;

        public Project CurrentProject { get; set; }
        public List<Project> Projects { get; set; }
        public Entrepreneur Entrepreneur { get; set; }


        private readonly IEntrepreneurRepository _entreprenurRepo;
        private readonly ICurrentUser _currentUser;

        public DetailModel(
            IEntrepreneurRepository entreprenurRepo,
            ICurrentUser currentUser
            )
        {
            this._currentUser = currentUser;
            this._entreprenurRepo = entreprenurRepo;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Entrepreneur = await _entreprenurRepo.GetWithDetailAsync(id);
            IsMyPage = _currentUser.Id == Entrepreneur.IdentityUserId;
            Projects = Entrepreneur.MyProjects != null && Entrepreneur.MyProjects.Any() ? Entrepreneur.MyProjects.ToList() : null;
            CurrentProject = Projects != null ? Projects.Where(a => a.Status ==  ProjectStatus.OnGoing).FirstOrDefault() : null;
        }
    }
}
