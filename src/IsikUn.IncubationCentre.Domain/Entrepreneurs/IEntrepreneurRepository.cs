using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Entrepreneurs
{
    public interface IEntrepreneurRepository : IRepository<Entrepreneur, Guid>
    {
        Task<List<Entrepreneur>> GetListAsync(
             string filter = null,
             string userName = null,
             string name = null,
             string surname = null,
             string email = null,
             string phoneNumber = null,
             string experience = null,
             Guid[] SkillIds = null,
             string about = null,
             bool filterByActiveted = false,
             bool isActivated = true,
             int skipCount = 0,
             int maxResultCount = int.MaxValue,
             string sorting = null,
             CancellationToken cancelationToken = default
         );

        Task<long> GetCountAsync(
            string filter = null,
             string userName = null,
             string name = null,
             string surname = null,
             string email = null,
             string phoneNumber = null,
             string experience = null,
             Guid[] SkillIds = null,
             string about = null,
             bool filterByActiveted = false,
             bool isActivated = true,
             CancellationToken cancelationToken = default
            );
        Task<Entrepreneur> GetWithDetailAsync(Guid id, CancellationToken cancelationToken = default);
    }
}
