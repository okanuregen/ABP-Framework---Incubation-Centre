using IsikUn.IncubationCentre.Events;
using IsikUn.IncubationCentre.Projects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace IsikUn.IncubationCentre.Web.Pages.Events
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateEntrepreneurEventModal Event { get; set; }

        public List<SelectListItem> Projects { get; set; }


        private readonly IEventAppService _service;
        private readonly IProjectRepository _projectRepo;
        private readonly IProjectAppService _projectService;

        public CreateModalModel(IEventAppService service, IProjectRepository projectRepo, IProjectAppService projectService)
        {
            this._service = service;
            this._projectRepo = projectRepo;
            this._projectService = projectService;
        }

        public async void OnGet()
        {

            var allProjects = await _projectService.GetListAsync(new GetProjectsInput());

            if (allProjects.TotalCount > 0)
            {
                var personProjects = allProjects.Items.ToList().Where(a =>
                    (a.Mentors != null ? a.Mentors.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) : false) ||
                    (a.Investors != null ? a.Investors.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) : false) ||
                    (a.Entrepreneurs != null ? a.Entrepreneurs.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) : false) ||
                    (a.Collaborators != null ? a.Collaborators.Select(b => b.IdentityUserId).Contains(CurrentUser.Id.Value) : false)
                );
                if (personProjects != null && personProjects.Any())
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
            else
            {
                Projects = new List<SelectListItem>();
                Projects.Add(new SelectListItem
                {
                    Text = L["NoFoundProject"],
                    Value = ""
                });
            }

            Event = new CreateEntrepreneurEventModal
            {
                EventDate = DateTime.Now,
            };


        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Event);
            return NoContent();
        }

        public class CreateEntrepreneurEventModal : CreateUpdateEventDto
        {

            [SelectItems(nameof(Projects))]
            [DisplayName("Project")]
            public override Guid ProjectId { get; set; }

        }
    }
}