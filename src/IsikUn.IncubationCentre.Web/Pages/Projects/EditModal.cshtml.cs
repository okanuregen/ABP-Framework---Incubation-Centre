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
using IsikUn.IncubationCentre.Currencies;
using IsikUn.IncubationCentre.Collaborators;

namespace IsikUn.IncubationCentre.Web.Pages.Projects
{

    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateProjectDto Project { get; set; }
        public List<SelectListItem> Currencies { get; set; }
        public List<SelectListItem> Collaborators { get; set; }


        private readonly ICurrencyRepository _currencyRepo;
        private readonly IProjectAppService _service;
        private readonly ICollaboratorRepository _collaboratorRepo;

        public EditModalModel(IProjectAppService service, ICurrencyRepository currencyRepo, ICollaboratorRepository collaboratorRepo)
        {
            this._collaboratorRepo = collaboratorRepo;
            _service = service;
            _currencyRepo = currencyRepo;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetWithDetailAsync(Id);
            Project = ObjectMapper.Map<ProjectDto, CreateUpdateProjectDto>(dto);
            Project.EntreprenurId = dto.Entrepreneurs.FirstOrDefault().Id;
            var currencies = await _currencyRepo.GetListAsync();
            if (currencies == null || currencies.Count() > 0)
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
                    a.Id.ToString(),
                    dto.Collaborators.Select(x => x.Id).Contains(a.Id)
                )).ToList();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Project);
            return NoContent();
        }

    }
}