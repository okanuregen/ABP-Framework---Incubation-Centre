using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
namespace IsikUn.IncubationCentre.Documents
{
    public interface IDocumentRepository : IRepository<Document, Guid>
    {
        Task<List<Document>> GetListAsync(
             string filter = null,
             string name = null,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
             string filter = null,
             string name = null,
             CancellationToken cancelationToken = default
            );
        
    }
}
