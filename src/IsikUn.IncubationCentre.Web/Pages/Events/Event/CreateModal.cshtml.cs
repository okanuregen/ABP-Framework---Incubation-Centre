using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Events;
using IsikUn.IncubationCentre.Events.Dtos;
using IsikUn.IncubationCentre.Web.Pages.Events.Event.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Events.Event
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateEventViewModel ViewModel { get; set; }

        private readonly IEventAppService _service;

        public CreateModalModel(IEventAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEventViewModel, CreateUpdateEventDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}