using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Requests;

namespace IsikUn.IncubationCentre.Web.Pages.Requests
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateRequestDto RequestDto { get; set; }

        private readonly IRequestAppService _service;

        public EditModalModel(IRequestAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            RequestDto = ObjectMapper.Map<RequestDto, CreateUpdateRequestDto>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, RequestDto);
            return NoContent();
        }
    }
}