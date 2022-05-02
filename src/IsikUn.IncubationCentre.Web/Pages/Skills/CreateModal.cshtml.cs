using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Skills
{
   
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateSkillDto Skill { get; set; }


        private readonly ISkillAppService _skillAppService;

        public CreateModalModel(ISkillAppService skillAppService)
        {
            this._skillAppService = skillAppService;
        }

        public void OnGet()
        {
            Skill = new CreateSkillDto();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _skillAppService.CreateAsync(Skill);
            return NoContent();
        }
    }

}
