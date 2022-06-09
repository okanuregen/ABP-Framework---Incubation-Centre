using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using IsikUn.IncubationCentre.Currencies;

namespace IsikUn.IncubationCentre.Web.Pages.Currencies.Currency
{
    public class CreateModalModel : IncubationCentrePageModel
    {
        [BindProperty]
        public CreateUpdateCurrencyDto ViewModel { get; set; }

        private readonly ICurrencyAppService _service;

        public CreateModalModel(ICurrencyAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ViewModel);
            return NoContent();
        }
    }
}