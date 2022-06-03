using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Applications;
namespace IsikUn.IncubationCentre.Web.Pages.Applications
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateApplicationDto Application { get; set; }

        private readonly IApplicationAppService _service;

        public CreateModalModel(IApplicationAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Application);
            return NoContent();
        }
    }
}