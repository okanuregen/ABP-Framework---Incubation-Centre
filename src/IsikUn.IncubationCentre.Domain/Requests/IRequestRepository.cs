using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Requests
{
    public interface IRequestRepository : IRepository<Request, Guid>
    {
        Task<List<Request>> GetListAsync(
             string filter = null,
             string title = null,
             string explanation = null,
             string senderUserName = null,
             string receiverUserName = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             string filter = null,
             string title = null,
             string explanation = null,
             string senderUserName = null,
             string receiverUserName = null,
             CancellationToken cancelationToken = default
            );

    }
}
