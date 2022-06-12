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
        public List<SelectListItem> Projects { get; set; }

        private readonly IEventAppService _service;
        private readonly IProjectRepository _projectRepo;

        public EditModalModel(IEventAppService service, IProjectRepository projectRepo)
        {
            _service = service;
            _projectRepo = projectRepo;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            Event = ObjectMapper.Map<EventDto, CreateUpdateEventDto>(dto);
            var allProjects = await _projectRepo.GetListAsync();
            var personProjects = allProjects.Where(a =>
                a.Mentors.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) ||
                a.Investors.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) ||
                a.Entrepreneurs.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) ||
                a.Collaborators.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value)
            );
            if (personProjects.Any())
            {
                Projects = personProjects.Select(a => new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                }).ToList();
            }
            else
            {
                Projects = new List<SelectListItem>();
                Projects.Add(new SelectListItem
                {
                    Text = L["NoFoundProject"],
                    Value = ""
                });
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, Event);
            return NoContent();
        }
    }
}