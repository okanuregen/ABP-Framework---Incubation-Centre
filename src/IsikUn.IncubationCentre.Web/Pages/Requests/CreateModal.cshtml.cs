using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Requests;

namespace IsikUn.IncubationCentre.Web.Pages.Requests
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateRequestDto RequestDto { get; set; }

        private readonly IRequestAppService _service;

        public CreateModalModel(IRequestAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(RequestDto);
            return NoContent();
        }
    }
}