using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.Mentors;
using Volo.Abp.Users;
using System.Linq;
using System;

namespace IsikUn.IncubationCentre.Web.Pages.Mentors
{
    public class DetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid id { get; set; }

        public bool IsMyPage = false;

        public List<Project> Projects { get; set; }
        public Mentor Mentor { get; set; }


        private readonly IMentorRepository _mentorRepo;
        private readonly ICurrentUser _currentUser;

        public DetailModel(
            IMentorRepository mentorRepo,
            ICurrentUser currentUser
            )
        {
            this._currentUser = currentUser;
            this._mentorRepo = mentorRepo;
        }
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Mentor = await _mentorRepo.GetWithDetailAsync(id);
            IsMyPage = _currentUser.Id == Mentor.IdentityUserId;
            Projects = Mentor.MentoringProjects != null && Mentor.MentoringProjects.Any() ? Mentor.MentoringProjects.ToList() : null;
        }
    }
}
