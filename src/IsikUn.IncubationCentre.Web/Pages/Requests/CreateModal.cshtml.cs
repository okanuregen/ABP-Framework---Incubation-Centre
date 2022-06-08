using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.People;
using Volo.Abp.Users;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace IsikUn.IncubationCentre.Web.Pages.Requests
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateRequestViewModel RequestDto { get; set; }

        public List<SelectListItem> Users { get; set; }


        private readonly IRequestAppService _service;
        private readonly IPersonRepository _personRepo;
        private readonly ICurrentUser _currentUser;

        public CreateModalModel(IRequestAppService service, IPersonRepository personRepo, ICurrentUser currentUser)
        {
            _service = service;
            _personRepo = personRepo;
            _currentUser = currentUser;
        }

        public async Task OnGet()
        {
            var users = await _personRepo.GetListAsync();
            Users = users.Select(x => new SelectListItem(string.Format("{0} ({1} {2})", x.IdentityUser.UserName, x.IdentityUser.Name, x.IdentityUser.Surname), x.Id.ToString())).ToList();
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var person = await _personRepo.GetWithDetailByIdentityUserIdAsync(_currentUser.Id.Value);
            RequestDto.SenderId = person.Id;
            await _service.CreateAsync(RequestDto);
            return NoContent();
        }


        public class CreateRequestViewModel : CreateUpdateRequestDto
        {
            [SelectItems(nameof(Users))]
            public override Guid? ReceiverId { get; set; }

            public override string Title { get; set; }
            
            [TextArea(Rows = 4)]
            public override string Explanation { get; set; }
        }
    }
}