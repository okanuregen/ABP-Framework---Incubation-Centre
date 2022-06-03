using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Tasks;
using IsikUn.IncubationCentre.Tasks.Dtos;
using IsikUn.IncubationCentre.Web.Pages.Tasks.Task.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Tasks.Task
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EditTaskViewModel ViewModel { get; set; }

        private readonly ITaskAppService _service;

        public EditModalModel(ITaskAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<TaskDto, EditTaskViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditTaskViewModel, UpdateTaskDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}