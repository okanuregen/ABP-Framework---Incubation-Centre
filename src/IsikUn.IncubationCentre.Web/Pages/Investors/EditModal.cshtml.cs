using IsikUn.IncubationCentre.Investors;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Web.Pages.Investors
{

    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateInvestorViewModel Investor { get; set; }


        public List<SelectListItem> Skills { get; set; }



        private readonly IInvestorAppService _investorAppService;
        private readonly IInvestorRepository _investorRepository;
        private readonly ISkillAppService _skillAppService;
        private readonly IPersonSkillRepository _personSkillRepository;

        public EditModalModel(
            IInvestorAppService investorAppService,
            IInvestorRepository investorRepository,
            ISkillAppService skillAppService,
            IPersonSkillRepository personSkillRepository)
        {
            this._investorAppService = investorAppService;
            this._investorRepository = investorRepository;
            this._skillAppService = skillAppService;
            this._personSkillRepository = personSkillRepository;
        }

        public async Task OnGet()
        {
            var investor = await _investorRepository.GetWithDetailAsync(Id);
            Investor = new UpdateInvestorViewModel
            {
                Id = Id,
                About = investor.About,
                Experience = investor.Experience,
                isActivated = investor.isActivated,
                IdentityUserId = investor.IdentityUserId.Value,
                SkillIds = investor.Skills.Select(a => a.Id).ToList(),
                CreationTime = investor.CreationTime

            };

            var skills = await _skillAppService.GetAllItemsAsync();
            Skills = skills
                .Select(x => new SelectListItem(x.Name, x.Id.ToString(), Investor.SkillIds.Contains(x.Id)))
                .ToList();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            Investor.Id = Id;
            var investor = await _investorAppService.UpdateAsync(Id, ObjectMapper.Map<UpdateInvestorViewModel, CreateUpdateInvestorDto>(Investor));

            List<Guid> deletedSkillIds = new List<Guid>();
            List<Guid> newSkillIds = new List<Guid>();

            if (investor.Skills == null || investor.Skills.Count() == 0) //current skills is empty
            {
                newSkillIds = Investor.SkillIds != null ? Investor.SkillIds : newSkillIds;
            }
            else
            {
                deletedSkillIds = Investor.SkillIds != null ? investor.Skills.Select(a => a.Id).Where(a => !Investor.SkillIds.Contains(a)).ToList() : investor.Skills.Select(a => a.Id).ToList();
                newSkillIds = Investor.SkillIds != null ? Investor.SkillIds.Where(a => !investor.Skills.Select(b => b.Id).Contains(a)).ToList() : newSkillIds;
            }

            if (deletedSkillIds.Any())
            {
                await _personSkillRepository.DeleteManyAsync(
                    await _personSkillRepository.GetListAsync(a => deletedSkillIds.Contains(a.SkillId))
                    , true);
            }

            //new added skills
            var skills = newSkillIds.Select(a => new PersonSkill
            {
                SkillId = a,
                PersonId = Id,
            });

            if (skills.Any())
            {
                await _personSkillRepository.InsertManyAsync(skills, true);
            }
            return NoContent();
        }
    }

    public class UpdateInvestorViewModel : CreateUpdateInvestorDto
    {

        [SelectItems(nameof(EditModalModel.Skills))]
        [DisplayName("Skills")]
        public override List<Guid> SkillIds { get; set; }

        [TextArea(Rows = 4)]
        public override string Experience { get => base.Experience; set => base.Experience = value; }

        [TextArea(Rows = 4)]
        public override string About { get => base.About; set => base.About = value; }

    
    }

}
