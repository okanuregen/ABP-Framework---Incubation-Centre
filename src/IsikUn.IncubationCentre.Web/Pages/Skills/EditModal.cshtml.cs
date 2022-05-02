using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Skills
{
   
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateSkillDto Skill { get; set; }



        private readonly ISkillAppService _skillAppService;

        public EditModalModel(ISkillAppService skillAppService)
        {
            this._skillAppService = skillAppService;
        }

        public async void OnGet()
        {
           var skill = await _skillAppService.GetAsync(Id);
            Skill = ObjectMapper.Map<SkillDto, UpdateSkillDto>(skill);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _skillAppService.UpdateAsync(Id,Skill);
            return NoContent();
        }
    }

}
