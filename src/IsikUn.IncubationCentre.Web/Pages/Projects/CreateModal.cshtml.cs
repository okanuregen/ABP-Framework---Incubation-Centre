using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Web.Pages.Projects
{

    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }

        private readonly IProjectAppService _projectAppService;
        public CreateModalModel(
            IProjectAppService projectAppService)
        {
            this._projectAppService = projectAppService;
        }

        public async Task OnGetAsync()
        {
            Project = new CreateUpdateProjectDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newProject = await _projectAppService.CreateAsync(Project);
            return NoContent();
        }
    }

}
