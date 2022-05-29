using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Documents;
using IsikUn.IncubationCentre.Web.Pages.Documents.ViewModels;

namespace IsikUn.IncubationCentre.Web.Pages.Documents
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateEditDocumentViewModel ViewModel { get; set; }

        private readonly IDocumentAppService _service;

        public CreateModalModel(IDocumentAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditDocumentViewModel, CreateUpdateDocumentDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}