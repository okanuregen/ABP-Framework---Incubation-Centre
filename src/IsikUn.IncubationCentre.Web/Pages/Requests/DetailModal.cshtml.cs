using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Requests;
using IsikUn.IncubationCentre.People;
using System.ComponentModel;

namespace IsikUn.IncubationCentre.Web.Pages.Requests
{
    public class DetailModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]

        [DisplayName("Sender")]
        public string SenderName { get; set; }
        
        [DisplayName("Sender")]
        public string ReceiverName { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }
        public string Explanation { get; set; }


        private readonly IRequestAppService _service;
        private readonly IPersonRepository _personRepo;

        public DetailModalModel(IRequestAppService service, IPersonRepository personRepo)
        {
            _service = service;
            _personRepo = personRepo;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            if(dto != null)
            {
                var sender = dto.SenderId.HasValue ? await _personRepo.GetWithDetailByIdAsync(dto.SenderId.Value) : null;
                var receiver = dto.ReceiverId.HasValue ? await _personRepo.GetWithDetailByIdAsync(dto.ReceiverId.Value) : null;

                SenderName = sender != null && sender.IdentityUser != null ?
                    String.Format("{0} {1} ({2})", sender.IdentityUser.Name, sender.IdentityUser.Surname, sender.IdentityUser.UserName) 
                    : "-";
                
                ReceiverName = receiver != null && receiver.IdentityUser != null ?
                    String.Format("{0} {1} ({2})", receiver.IdentityUser.Name, receiver.IdentityUser.Surname, receiver.IdentityUser.UserName) 
                    : "-";

                Title = dto.Title;
                Explanation = dto.Explanation;

            }
            else
            {
                SenderName = "-";
                ReceiverName = "-";
                Title = "-";
                Explanation = "-";
            }


        }

    }
}