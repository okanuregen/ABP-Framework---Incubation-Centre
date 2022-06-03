using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.Requests.Dtos;
using IsikUn.IncubationCentre.Web.Pages.Requests.Request.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Requests.Request
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EditRequestViewModel ViewModel { get; set; }

        private readonly IRequestAppService _service;

        public EditModalModel(IRequestAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<RequestDto, EditRequestViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditRequestViewModel, UpdateRequestDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}