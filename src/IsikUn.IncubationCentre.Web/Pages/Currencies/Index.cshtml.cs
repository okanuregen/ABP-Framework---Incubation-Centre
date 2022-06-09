using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Currencies.Currency
{
    public class IndexModel : IncubationCentrePageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
