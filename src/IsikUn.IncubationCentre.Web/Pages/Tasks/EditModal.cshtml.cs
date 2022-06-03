using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Tasks
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateTaskDto Task { get; set; }

        private readonly ITaskAppService _service;

        public EditModalModel(ITaskAppService service)
        {
            _service = service;
        }

        public virtual async System.Threading.Tasks.Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            Task = ObjectMapper.Map<TaskDto, CreateUpdateTaskDto>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Task);
            return NoContent();
        }
    }
}