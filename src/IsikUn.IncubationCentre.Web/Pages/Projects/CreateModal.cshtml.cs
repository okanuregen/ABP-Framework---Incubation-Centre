using IsikUn.IncubationCentre.Collaborators;
using IsikUn.IncubationCentre.Currencies;
using IsikUn.IncubationCentre.Entrepreneurs;
using IsikUn.IncubationCentre.People;
using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Users;

namespace IsikUn.IncubationCentre.Web.Pages.Projects
{

    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }
        public List<SelectListItem> Currencies { get; set; }
        public List<SelectListItem> Collaborators { get; set; }


        private readonly IProjectAppService _projectAppService;
        private readonly ICollaboratorRepository _collaboratorRepo;
        private readonly ICurrencyRepository _currencyRepo;
        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;

        public CreateModalModel(
            IProjectAppService projectAppService,
            ICollaboratorRepository collaboratorRepo,
            ICurrencyRepository currencyRepo,
            IPersonRepository personRepo,
            ICurrentUser currentUser)
        {
            this._collaboratorRepo = collaboratorRepo;
            this._currencyRepo = currencyRepo;
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
            var currencies = await _currencyRepo.GetListAsync();
            if(currencies == null || currencies.Count() > 0)
            {
                Currencies = new List<SelectListItem>();
                Currencies.Add(new SelectListItem
                {
                    Text = L["TurkishLira"].Value + " - " + "₺",
                    Value = "₺"
                });
            }
            else
            {
            Currencies = currencies.Select(a =>
                new SelectListItem
                {
                    Selected = a.IsDefault,
                    Value = a.Symbol,
                    Text = (a.Title + " - " + a.Symbol)
                }).ToList();
            }

            var collabs = await _collaboratorRepo.GetListAsync();
            Collaborators = collabs.Select(a =>
                new SelectListItem(
                    string.Format("{0} {1} ({2})", a.IdentityUser.Name, a.IdentityUser.Surname, a.IdentityUser.UserName),
                    a.Id.ToString()
                )).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _projectAppService.CreateAsync(Project);
            return NoContent();
        }
    }

}
