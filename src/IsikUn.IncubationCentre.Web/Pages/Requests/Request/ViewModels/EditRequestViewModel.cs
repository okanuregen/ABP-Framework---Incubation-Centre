using System;

using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Web.Pages.Requests.Request.ViewModels
{
    public class EditRequestViewModel
    {
        [Display(Name = "RequestSenderId")]
        public Guid SenderId { get; set; }

        [Display(Name = "RequestSender")]
        public Person Sender { get; set; }

        [Display(Name = "RequestReceiverId")]
        public Guid ReceiverId { get; set; }

        [Display(Name = "RequestReceiver")]
        public Person Receiver { get; set; }

        [Display(Name = "RequestTitle")]
        public string Title { get; set; }

        [Display(Name = "RequestExplanation")]
        public string Explanation { get; set; }
    }
}