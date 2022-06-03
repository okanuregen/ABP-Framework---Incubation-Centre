using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Tasks;
using IsikUn.IncubationCentre.Tasks.Dtos;
using IsikUn.IncubationCentre.Web.Pages.Tasks.Task.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Tasks.Task
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateTaskViewModel ViewModel { get; set; }

        private readonly ITaskAppService _service;

        public CreateModalModel(ITaskAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateTaskViewModel, CreateUpdateTaskDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}