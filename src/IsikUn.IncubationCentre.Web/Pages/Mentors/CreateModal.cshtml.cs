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

    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateMentorViewModel Mentor { get; set; }


        public List<SelectListItem> Skills { get; set; }
        public List<SelectListItem> Users { get; set; }



        private readonly IMentorAppService _mentorAppService;
        private readonly ISkillAppService _skillAppService;
        private readonly IIdentityUserRepository _identityUserRepository;
        private readonly IIdentityRoleRepository _identityRoleRepository;
        private readonly IPersonSkillRepository _personSkillRepository;

        public CreateModalModel(
            IMentorAppService mentorAppService,
            ISkillAppService skillAppService,
            IIdentityUserRepository identityUserRepository,
            IIdentityRoleRepository identityRoleRepository,
            IPersonSkillRepository personSkillRepository)
        {
            this._mentorAppService = mentorAppService;
            this._skillAppService = skillAppService;
            this._identityUserRepository = identityUserRepository;
            this._identityRoleRepository = identityRoleRepository;
            this._personSkillRepository = personSkillRepository;
        }

        public async Task OnGetAsync()
        {
            Mentor = new CreateMentorViewModel();

            var skills = await _skillAppService.GetAllItemsAsync();
            Skills = skills
                .Select(x => new SelectListItem(x.Name, x.Id.ToString()))
                .ToList();

            var mentorRole = (await _identityRoleRepository.GetListAsync(filter: "Mentor")).FirstOrDefault();
            if (mentorRole != null)
            {
                var users = await _identityUserRepository.GetListAsync();
                Users = users.Where(a => (_identityUserRepository.GetRolesAsync(a.Id).Result).Select(b=> b.Id).Contains(mentorRole.Id))
                        .Select(x => new SelectListItem(string.Format("{0} {1}", x.Name, x.Surname), x.Id.ToString()))
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
            var newMentor = await _mentorAppService.CreateAsync(ObjectMapper.Map<CreateMentorViewModel, CreateUpdateMentorDto>(Mentor));
            var skills = Mentor.SkillIds != null ? Mentor.SkillIds.Select(a => new PersonSkill
            {
                SkillId = a,
                PersonId = newMentor.Id
            }) : new List<PersonSkill>();
            if (skills.Any())
            {
                await _personSkillRepository.InsertManyAsync(skills,true);
            }
            return NoContent();
        }

        public class CreateMentorViewModel : CreateUpdateMentorDto
        {

            [SelectItems(nameof(Skills))]
            [DisplayName("Skills")]
            public override List<Guid> SkillIds { get; set; }

            [Required]
            [SelectItems(nameof(Users))]
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

}
