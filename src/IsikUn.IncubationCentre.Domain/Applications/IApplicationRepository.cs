using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace IsikUn.IncubationCentre.Applications
{
    public interface IApplicationRepository : IRepository<Application, Guid>
    {
        Task<List<Application>> GetListAsync(
            string filter = null,
            string senderName = null,
            string senderSurname = null,
            string senderMail = null,
            string senderPhoneNumber = null,
            string explanation = null,
            string applicationStatus = null,
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string sorting = null,
            CancellationToken cancelationToken = default
        );

        Task<long> GetCountAsync(
            string filter = null,
            string senderName = null,
            string senderSurname = null,
            string senderMail = null,
            string senderPhoneNumber = null,
            string explanation = null,
            string applicationStatus = null,
            CancellationToken cancelationToken = default
            );

    }
}