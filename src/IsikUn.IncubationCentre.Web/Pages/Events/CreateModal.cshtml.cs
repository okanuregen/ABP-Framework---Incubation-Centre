using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Events;


namespace IsikUn.IncubationCentre.Web.Pages.Events
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateEventDto Event { get; set; }

        private readonly IEventAppService _service;

        public CreateModalModel(IEventAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Event);
            return NoContent();
        }
    }
}