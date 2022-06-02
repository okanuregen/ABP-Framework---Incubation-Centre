using IsikUn.IncubationCentre.Projects;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
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

    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }

        private readonly IProjectAppService _service;

        public EditModalModel(IProjectAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            Project = ObjectMapper.Map<ProjectDto, CreateUpdateProjectDto>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Project);
            return NoContent();
        }
    }
}