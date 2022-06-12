using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Events;
using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace IsikUn.IncubationCentre.Web.Pages.Events
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateEventDto Event { get; set; }

        private readonly IEventAppService _service;

        public EditModalModel(IEventAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            Event = ObjectMapper.Map<EventDto, CreateUpdateEventDto>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Event);
            return NoContent();
        }
    }
}