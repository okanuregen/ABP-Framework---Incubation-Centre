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


        private readonly IProjectAppService _projectAppService;
        private readonly ICurrencyRepository _currencyRepo;
        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;

        public CreateModalModel(
            IProjectAppService projectAppService,
            ICurrencyRepository currencyRepo,
            IPersonRepository personRepo,
            ICurrentUser currentUser)
        {
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
            Currencies = currencies.Select(a =>
                new SelectListItem
                {
                    Selected = a.IsDefault,
                    Value = a.Symbol,
                    Text = (a.Title + " - " + a.Symbol)
                }).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _projectAppService.CreateAsync(Project);
            return NoContent();
        }
    }

}
