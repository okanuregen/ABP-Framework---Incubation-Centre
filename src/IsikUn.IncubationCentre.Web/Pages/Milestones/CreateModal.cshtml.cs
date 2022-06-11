using IsikUn.IncubationCentre.Milestones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Milestones
{
   
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateMilestoneDto Milestone { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProjectId { get; set; }


        private readonly IMilestoneAppService _AppService;

        public CreateModalModel(IMilestoneAppService AppService)
        {
            this._AppService = AppService;
        }

        public void OnGet()
        {
            Milestone = new CreateUpdateMilestoneDto { 
                ProjectId = ProjectId,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _AppService.CreateAsync(Milestone);
            return NoContent();
        }
    }

}
