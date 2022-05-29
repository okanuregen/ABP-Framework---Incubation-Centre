using System;

using System.ComponentModel.DataAnnotations;

namespace IsikUn.IncubationCentre.Web.Pages.Documents.ViewModels
{
    public class CreateEditDocumentViewModel
    {
        [Display(Name = "DocumentName")]
        public string Name { get; set; }
    }
}