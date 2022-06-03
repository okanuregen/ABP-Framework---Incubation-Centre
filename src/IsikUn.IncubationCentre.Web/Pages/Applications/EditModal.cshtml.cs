using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Applications;

namespace IsikUn.IncubationCentre.Web.Pages.Applications
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateApplicationDto Application { get; set; }

        private readonly IApplicationAppService _service;

        public EditModalModel(IApplicationAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            Application = ObjectMapper.Map<ApplicationDto, CreateUpdateApplicationDto>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Application);
            return NoContent();
        }
    }
}