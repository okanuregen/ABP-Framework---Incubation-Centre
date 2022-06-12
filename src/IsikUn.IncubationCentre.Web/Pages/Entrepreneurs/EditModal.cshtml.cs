using IsikUn.IncubationCentre.Entrepreneurs;
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

namespace IsikUn.IncubationCentre.Web.Pages.Entrepreneurs
{

    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateEntrepreneurViewModel Entrepreneur { get; set; }


        public List<SelectListItem> Skills { get; set; }
        public List<SelectListItem> Users { get; set; }



        private readonly IEntrepreneurAppService _entrepreneurAppService;
        private readonly IEntrepreneurRepository _entrepreneurRepository;
        private readonly ISkillAppService _skillAppService;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IPersonSkillRepository _personSkillRepository;

        public EditModalModel(
            IEntrepreneurAppService entrepreneurAppService,
            IEntrepreneurRepository entrepreneurRepository,
            ISkillAppService skillAppService,
            IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IPersonSkillRepository personSkillRepository)
        {
            this._entrepreneurAppService = entrepreneurAppService;
            this._entrepreneurRepository = entrepreneurRepository;
            this._skillAppService = skillAppService;
            this._identityUserRepository = identityUserRepository;
            this._identityRoleRepository = identityRoleRepository;
            this._personSkillRepository = personSkillRepository;
        }

        public async Task OnGet()
        {
            var entrepreneur = await _entrepreneurRepository.GetWithDetailAsync(Id);
            Entrepreneur = new UpdateEntrepreneurViewModel
            {
                Id = Id,
                About = entrepreneur.About,
                Experience = entrepreneur.Experience,
                isActivated = entrepreneur.isActivated,
                IdentityUserId = entrepreneur.IdentityUserId.Value,
                SkillIds = entrepreneur.Skills.Select(a => a.Id).ToList(),
                CreationTime = entrepreneur.CreationTime
            };

            var skills = await _skillAppService.GetAllItemsAsync();
            Skills = skills
                .Select(x => new SelectListItem(x.Name, x.Id.ToString(), Entrepreneur.SkillIds.Contains(x.Id)))
                .ToList();

            var entrepreneurRole = (await _identityRoleRepository.GetListAsync(filter: "Entrepreneur")).FirstOrDefault();
            if (entrepreneurRole != null)
            {
                var users = await _identityUserRepository.GetListAsync();
                Users = users.Where(a => (_identityUserRepository.GetRolesAsync(a.Id).Result).Select(b => b.Id).Contains(entrepreneurRole.Id))
                        .Select(x => new SelectListItem(string.Format("{0} ({1} {2})", x.UserName, x.Name, x.Surname), x.Id.ToString(), x.Id == Entrepreneur.IdentityUserId))
                        .ToList();
            }
            else
            {
                Users = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = L["NoUserFoundWithThisRole",L["Entrepreneur"]],
                        Value = ""
                    }
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Entrepreneur.Id = Id;
            var entrepreneur = await _entrepreneurAppService.UpdateAsync(Id, ObjectMapper.Map<UpdateEntrepreneurViewModel, CreateUpdateEntrepreneurDto>(Entrepreneur));

            List<Guid> deletedSkillIds = new List<Guid>();
            List<Guid> newSkillIds = new List<Guid>();

            if (entrepreneur.Skills == null || entrepreneur.Skills.Count() == 0) //current skills is empty
            {
                newSkillIds = Entrepreneur.SkillIds != null ? Entrepreneur.SkillIds : newSkillIds;
            }
            else
            {
                deletedSkillIds = Entrepreneur.SkillIds != null ? entrepreneur.Skills.Select(a => a.Id).Where(a => !Entrepreneur.SkillIds.Contains(a)).ToList() : entrepreneur.Skills.Select(a => a.Id).ToList();
                newSkillIds = Entrepreneur.SkillIds != null ? Entrepreneur.SkillIds.Where(a => !entrepreneur.Skills.Select(b => b.Id).Contains(a)).ToList() : newSkillIds;
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

    public class UpdateEntrepreneurViewModel : CreateUpdateEntrepreneurDto
    {

        [SelectItems(nameof(EditModalModel.Skills))]
        [DisplayName("Skills")]
        public override List<Guid> SkillIds { get; set; }

        [Required]
        [SelectItems(nameof(EditModalModel.Users))]
        [DisplayName("User")]
        public override Guid IdentityUserId { get; set; }

        [TextArea(Rows = 4)]
        public override string Experience { get => base.Experience; set => base.Experience = value; }

        [TextArea(Rows = 4)]
        public override string About { get => base.About; set => base.About = value; }

        [DisplayName("isActive")]
        public override bool isActivated { get => base.isActivated; set => base.isActivated = value; }
    }

}
