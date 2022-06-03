using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Requests.Dtos;
using IsikUn.IncubationCentre.Web.Pages.Requests.Request.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Requests.Request
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateRequestViewModel ViewModel { get; set; }

        private readonly IRequestAppService _service;

        public CreateModalModel(IRequestAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateRequestViewModel, CreateUpdateRequestDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}