using IsikUn.IncubationCentre.Milestones;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Milestones
{
   
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateMilestoneDto Milestone { get; set; }



        private readonly IMilestoneAppService _milestoneAppService;

        public EditModalModel(IMilestoneAppService milestoneAppService)
        {
            this._milestoneAppService = milestoneAppService;
        }

        public async void OnGet()
        {
           var milestone = await _milestoneAppService.GetAsync(Id);
            Milestone = ObjectMapper.Map<MilestoneDto, CreateUpdateMilestoneDto>(milestone);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _milestoneAppService.UpdateAsync(Id,Milestone);
            return NoContent();
        }
    }

}
