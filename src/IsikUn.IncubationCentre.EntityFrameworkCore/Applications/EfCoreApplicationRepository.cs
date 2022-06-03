using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using IsikUn.IncubationCentre.EntityFrameworkCore;


namespace IsikUn.IncubationCentre.Applications
{
    public class EfCoreApplicationRepository : EfCoreRepository<IncubationCentreDbContext, Application, Guid>,
        IApplicationRepository
    {
        public EfCoreApplicationRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider) : 
            base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string filter = null, string senderName = null, string senderSurname = null, string senderMail = null, string senderPhoneNumber = null, string explanation = null, string applicationStatus = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                    await GetQueryableAsync(), filter, senderName, senderSurname, senderMail, senderPhoneNumber, explanation, applicationStatus);
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<Application>> GetListAsync(string filter = null, string senderName = null, string senderSurname = null, string senderMail = null, string senderPhoneNumber = null, string explanation = null, string applicationStatus = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                    await GetQueryableAsync(), filter, senderName, senderSurname, senderMail, senderPhoneNumber, explanation, applicationStatus);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "CreationTime asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<Application> ApplyFilter(
            IQueryable<Application> query,
            string filter = null,
            string senderName = null,
            string senderSurname = null,
            string senderMail = null,
            string senderPhoneNumber = null,
            string explanation = null,
            string applicationStatus = null
          )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter), e => 
                        e.SenderName.Contains(filter) ||
                        e.SenderSurname.Contains(filter) ||
                        e.SenderMail.Contains(filter) ||
                        e.SenderPhoneNumber.Contains(filter) ||
                        e.Explanation.Contains(filter) ||
                        e.ApplicationStatus.ToString().Contains(filter)
                    )
                    .WhereIf(!string.IsNullOrWhiteSpace(senderName), e => e.SenderName.Contains(senderName))
                    .WhereIf(!string.IsNullOrWhiteSpace(senderSurname), e => e.SenderSurname.Contains(senderSurname))
                    .WhereIf(!string.IsNullOrWhiteSpace(senderMail), e => e.SenderMail.Contains(senderMail))
                    .WhereIf(!string.IsNullOrWhiteSpace(senderPhoneNumber), e => e.SenderPhoneNumber.Contains(senderPhoneNumber))
                    .WhereIf(!string.IsNullOrWhiteSpace(explanation), e => e.Explanation.Contains(explanation))
                    .WhereIf(!string.IsNullOrWhiteSpace(applicationStatus), e => e.ApplicationStatus.ToString().Contains(applicationStatus));
        }
    }
}