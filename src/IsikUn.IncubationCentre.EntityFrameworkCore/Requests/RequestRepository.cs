using System;
using System.Linq;
using System.Threading.Tasks;
using IsikUn.IncubationCentre.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace IsikUn.IncubationCentre.Requests
{
    public class RequestRepository : EfCoreRepository<IncubationCentreDbContext, Request, Guid>, IRequestRepository
    {
        public RequestRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Request>> WithDetailsAsync()
        {
            return (await GetQueryableAsync()).IncludeDetails();
        }
    }
}
