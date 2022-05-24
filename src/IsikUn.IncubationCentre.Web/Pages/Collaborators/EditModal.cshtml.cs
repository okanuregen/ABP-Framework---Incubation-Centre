using IsikUn.IncubationCentre.Collaborators;
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

namespace IsikUn.IncubationCentre.Web.Pages.Collaborators
{

    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateCollaboratorViewModel Collaborator { get; set; }


        public List<SelectListItem> Skills { get; set; }
        public List<SelectListItem> Users { get; set; }



        private readonly ICollaboratorAppService _collaboratorAppService;
        private readonly ICollaboratorRepository _collaboratorRepository;
        private readonly ISkillAppService _skillAppService;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IPersonSkillRepository _personSkillRepository;

        public EditModalModel(
            ICollaboratorAppService collaboratorAppService,
            ICollaboratorRepository collaboratorRepository,
            ISkillAppService skillAppService,
            IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IPersonSkillRepository personSkillRepository)
        {
            this._collaboratorAppService = collaboratorAppService;
            this._collaboratorRepository = collaboratorRepository;
            this._skillAppService = skillAppService;
            this._identityUserRepository = identityUserRepository;
            this._identityRoleRepository = identityRoleRepository;
            this._personSkillRepository = personSkillRepository;
        }

        public async Task OnGet()
        {
            var collaborator = await _collaboratorRepository.GetWithDetailAsync(Id);
            Collaborator = new UpdateCollaboratorViewModel
            {
                Id = Id,
                About = collaborator.About,
                Experience = collaborator.Experience,
                isActivated = collaborator.isActivated,
                IdentityUserId = collaborator.IdentityUserId.Value,
                SkillIds = collaborator.Skills.Select(a => a.Id).ToList()
            };

            var skills = await _skillAppService.GetAllItemsAsync();
            Skills = skills
                .Select(x => new SelectListItem(x.Name, x.Id.ToString(), Collaborator.SkillIds.Contains(x.Id)))
                .ToList();

            var collaboratorRole = (await _identityRoleRepository.GetListAsync(filter: "Collaborator")).FirstOrDefault();
            if (collaboratorRole != null)
            {
                var users = await _identityUserRepository.GetListAsync();
                Users = users.Where(a => (_identityUserRepository.GetRolesAsync(a.Id).Result).Select(b => b.Id).Contains(collaboratorRole.Id))
                        .Select(x => new SelectListItem(string.Format("{0} ({1} {2})", x.UserName, x.Name, x.Surname), x.Id.ToString(), x.Id == Collaborator.IdentityUserId))
                        .ToList();
            }
            else
            {
                Users = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = L["NoUserFoundWithThisRole",L["Collaborator"]],
                        Value = ""
                    }
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Collaborator.Id = Id;
            var collaborator = await _collaboratorAppService.UpdateAsync(Id, ObjectMapper.Map<UpdateCollaboratorViewModel, CreateUpdateCollaboratorDto>(Collaborator));

            List<Guid> deletedSkillIds = new List<Guid>();
            List<Guid> newSkillIds = new List<Guid>();

            if (collaborator.Skills == null || collaborator.Skills.Count() == 0) //current skills is empty
            {
                newSkillIds = Collaborator.SkillIds != null ? Collaborator.SkillIds : newSkillIds;
            }
            else
            {
                deletedSkillIds = Collaborator.SkillIds != null ? collaborator.Skills.Select(a => a.Id).Where(a => !Collaborator.SkillIds.Contains(a)).ToList() : collaborator.Skills.Select(a => a.Id).ToList();
                newSkillIds = Collaborator.SkillIds != null ? Collaborator.SkillIds.Where(a => !collaborator.Skills.Select(b => b.Id).Contains(a)).ToList() : newSkillIds;
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

    public class UpdateCollaboratorViewModel : CreateUpdateCollaboratorDto
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
