using System.Threading.Tasks;

namespace IsikUn.IncubationCentre.Web.Pages.Tasks
{
    public class IndexModel : IncubationCentrePageModel
    {
        public virtual async Task OnGetAsync()
        {
            await Task.CompletedTask;
        }
    }
}
