using System;
using System.Linq;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Events
{
    public class EventRepository : EfCoreRepository<IncubationCentreDbContext, Event, Guid>, IEventRepository
    {
        public EventRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Event>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
