using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.Users;

namespace IsikUn.IncubationCentre.Web.Pages.Projects
{

    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }

        private readonly IProjectAppService _projectAppService;
        private readonly IEntrepreneurRepository _entreprenurRepo;
        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;

        public CreateModalModel(
            IProjectAppService projectAppService,
            IEntrepreneurRepository entreprenurRepo,
            IPersonRepository personRepo,
            ICurrentUser currentUser)
        {
            this._entreprenurRepo = entreprenurRepo;
            this._projectAppService = projectAppService;
            this._personRepo = personRepo;
            this._currentUser = currentUser;
        }

        public async Task OnGetAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.GetId());
            Project = new CreateUpdateProjectDto();
            if(person != null)
            {
                Project.EntreprenurId = person.Id;
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var newProject = await _projectAppService.CreateAsync(Project);
            return NoContent();
        }
    }

}
