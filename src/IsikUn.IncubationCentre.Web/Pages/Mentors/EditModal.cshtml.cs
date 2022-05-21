using IsikUn.IncubationCentre.Mentors;
using IsikUn.IncubationCentre.PeopleSkills;
using IsikUn.IncubationCentre.Skills;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.Identity;

namespace IsikUn.IncubationCentre.Web.Pages.Mentors
{
   
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateMentorViewModel Mentor { get; set; }


        public List<SelectListItem> Skills { get; set; }
        public List<SelectListItem> Users { get; set; }



        private readonly IMentorAppService _mentorAppService;
        private readonly IMentorRepository _mentorRepository;
        private readonly ISkillAppService _skillAppService;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IPersonSkillRepository _personSkillRepository;

        public EditModalModel(
            IMentorAppService mentorAppService,
            IMentorRepository mentorRepository,
            ISkillAppService skillAppService,
            IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IPersonSkillRepository personSkillRepository)
        {
            this._mentorAppService = mentorAppService;
            this._mentorRepository = mentorRepository;
            this._skillAppService = skillAppService;
            this._identityUserRepository = identityUserRepository;
            this._identityRoleRepository = identityRoleRepository;
            this._personSkillRepository = personSkillRepository;
        }

        public async Task OnGet()
        {
            var mentor = await _mentorRepository.GetWithDetailAsync(Id);
            Mentor = new UpdateMentorViewModel
            {
                Id = Id,
                About = mentor.About,
                Experience = mentor.Experience,
                isActivated = mentor.isActivated,
                IdentityUserId = mentor.IdentityUserId.Value,
                SkillIds = mentor.Skills.Select(a => a.Id).ToList()
            };

            var skills = await _skillAppService.GetAllItemsAsync();
            Skills = skills
                .Select(x => new SelectListItem(x.Name, x.Id.ToString(), Mentor.SkillIds.Contains(x.Id)))
                .ToList();

            var mentorRole = (await _identityRoleRepository.GetListAsync(filter: "Mentor")).FirstOrDefault();
            if (mentorRole != null)
            {
                var users = await _identityUserRepository.GetListAsync();
                Users = users.Where(a => (_identityUserRepository.GetRolesAsync(a.Id).Result).Select(b => b.Id).Contains(mentorRole.Id))
                        .Select(x => new SelectListItem(string.Format("{0} {1}", x.Name, x.Surname), x.Id.ToString(), x.Id == Mentor.IdentityUserId))
                        .ToList();
            }
            else
            {
                Users = new List<SelectListItem>
                {
                    new SelectListItem
                    {
                        Text = "Mentor Rolunde Kullanýcý Bulunamadý",
                        Value = ""
                    }
                };
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var mentor = await _mentorAppService.UpdateAsync(Id, ObjectMapper.Map<UpdateMentorViewModel, CreateUpdateMentorDto>(Mentor));
            var skills = Mentor.SkillIds.Select(a => new PersonSkill
            {
                SkillId = a,
                PersonId = Id
            });
            await _personSkillRepository.DeleteManyAsync(mentor.Skills.Select(a => a.Id),true);
            if (skills.Any())
            {
                await _personSkillRepository.InsertManyAsync(skills, true);
            }
            return NoContent();
        }
    }

    public class UpdateMentorViewModel : CreateUpdateMentorDto
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
