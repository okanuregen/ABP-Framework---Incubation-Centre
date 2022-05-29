using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Web.Pages.Documents.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Documents
{
    public class EditModalModel : IncubationCentrePageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditDocumentViewModel ViewModel { get; set; }

        private readonly IDocumentAppService _service;

        public EditModalModel(IDocumentAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<DocumentDto, CreateEditDocumentViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditDocumentViewModel, CreateUpdateDocumentDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}