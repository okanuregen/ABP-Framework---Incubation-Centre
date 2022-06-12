using IsikUn.IncubationCentre.SystemManagers;
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

namespace IsikUn.IncubationCentre.Web.Pages.SystemManagers
{

    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateSystemManagerViewModel SystemManager { get; set; }


        public List<SelectListItem> Skills { get; set; }
        public List<SelectListItem> Users { get; set; }



        private readonly ISystemManagerAppService _systemManagerAppService;
        private readonly ISystemManagerRepository _systemManagerRepository;
        private readonly ISkillAppService _skillAppService;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IPersonSkillRepository _personSkillRepository;

        public EditModalModel(
            ISystemManagerAppService systemManagerAppService,
            ISystemManagerRepository systemManagerRepository,
            ISkillAppService skillAppService,
            IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IPersonSkillRepository personSkillRepository)
        {
            this._systemManagerAppService = systemManagerAppService;
            this._systemManagerRepository = systemManagerRepository;
            this._skillAppService = skillAppService;
            this._identityUserRepository = identityUserRepository;
            this._identityRoleRepository = identityRoleRepository;
            this._personSkillRepository = personSkillRepository;
        }

        public async Task OnGet()
        {
            var systemManager = await _systemManagerRepository.GetWithDetailAsync(Id);
            SystemManager = new UpdateSystemManagerViewModel
            {
                Id = Id,
                About = systemManager.About,
                Experience = systemManager.Experience,
                isActivated = systemManager.isActivated,
                IdentityUserId = systemManager.IdentityUserId.Value,
                SkillIds = systemManager.Skills.Select(a => a.Id).ToList(),
                CreationTime = systemManager.CreationTime
            };

            var skills = await _skillAppService.GetAllItemsAsync();
            Skills = skills
                .Select(x => new SelectListItem(x.Name, x.Id.ToString(), SystemManager.SkillIds.Contains(x.Id)))
                .ToList();

            var systemManagerRole = (await _identityRoleRepository.GetListAsync(filter: "System Manager")).FirstOrDefault();
            if (systemManagerRole != null)
            {
                var users = await _identityUserRepository.GetListAsync();
                Users = users.Where(a => (_identityUserRepository.GetRolesAsync(a.Id).Result).Select(b => b.Id).Contains(systemManagerRole.Id))
                        .Select(x => new SelectListItem(string.Format("{0} ({1} {2})", x.UserName, x.Name, x.Surname), x.Id.ToString(), x.Id == SystemManager.IdentityUserId))
                        .ToList();
            }
            else
            {
                Users = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = L["NoUserFoundWithThisRole",L["SystemManager"]],
                        Value = ""
                    }
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SystemManager.Id = Id;
            var systemManager = await _systemManagerAppService.UpdateAsync(Id, ObjectMapper.Map<UpdateSystemManagerViewModel, CreateUpdateSystemManagerDto>(SystemManager));

            List<Guid> deletedSkillIds = new List<Guid>();
            List<Guid> newSkillIds = new List<Guid>();

            if (systemManager.Skills == null || systemManager.Skills.Count() == 0) //current skills is empty
            {
                newSkillIds = SystemManager.SkillIds != null ? SystemManager.SkillIds : newSkillIds;
            }
            else
            {
                deletedSkillIds = SystemManager.SkillIds != null ? systemManager.Skills.Select(a => a.Id).Where(a => !SystemManager.SkillIds.Contains(a)).ToList() : systemManager.Skills.Select(a => a.Id).ToList();
                newSkillIds = SystemManager.SkillIds != null ? SystemManager.SkillIds.Where(a => !systemManager.Skills.Select(b => b.Id).Contains(a)).ToList() : newSkillIds;
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

    public class UpdateSystemManagerViewModel : CreateUpdateSystemManagerDto
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
