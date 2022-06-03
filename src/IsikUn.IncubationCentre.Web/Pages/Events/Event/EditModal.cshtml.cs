using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Events;
using IsikUn.IncubationCentre.Events.Dtos;
using IsikUn.IncubationCentre.Web.Pages.Events.Event.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Events.Event
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EditEventViewModel ViewModel { get; set; }

        private readonly IEventAppService _service;

        public EditModalModel(IEventAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<EventDto, EditEventViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditEventViewModel, UpdateEventDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}