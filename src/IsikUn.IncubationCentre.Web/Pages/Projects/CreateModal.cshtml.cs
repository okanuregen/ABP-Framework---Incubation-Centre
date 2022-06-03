using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
