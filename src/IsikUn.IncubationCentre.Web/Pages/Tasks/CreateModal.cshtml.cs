using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Tasks
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateTaskDto Task { get; set; }

        private readonly ITaskAppService _service;

        public CreateModalModel(ITaskAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Task);
            return NoContent();
        }
    }
}