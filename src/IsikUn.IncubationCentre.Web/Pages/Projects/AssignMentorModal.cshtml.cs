using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Projects
{

    public class AssignMentorModalModel : IncubationCentrePageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid projectId { get; set; }
        public ProjectDto Project { get; set; }
        public List<Mentor> Mentors { get; set; }


        private readonly IProjectAppService _projectAppService;
        private readonly IMentorRepository _mentorRepo;

        public AssignMentorModalModel(
            IProjectAppService projectAppService,
            IMentorRepository mentorRepo
            )
        {
            this._projectAppService = projectAppService;
            this._mentorRepo = mentorRepo;
        }

        public async Task OnGetAsync()
        {
            Project = await _projectAppService.GetWithDetailAsync(projectId);
            Mentors = await _mentorRepo.GetListAsync();
        }

    }

}
