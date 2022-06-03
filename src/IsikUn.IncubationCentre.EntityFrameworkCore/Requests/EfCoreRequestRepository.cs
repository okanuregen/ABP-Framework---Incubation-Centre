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

namespace IsikUn.IncubationCentre.Requests
{
    public class EfCoreRequestRepository : EfCoreRepository<IncubationCentreDbContext, Request, Guid>,
        IRequestRepository
    {
        public EfCoreRequestRepository(IDbContextProvider<IncubationCentreDbContext> dbContextProvider)
           : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync(string filter = null, string title = null, string explanation = null, string senderUserName = null, string receiverUserName = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                                (await GetQueryableAsync())
                                    .Include(a => a.Sender)
                                    .Include(b => b.Receiver), filter, title, explanation, senderUserName, receiverUserName);
            return await query.LongCountAsync(GetCancellationToken(cancelationToken));
        }

        public async Task<List<Request>> GetListAsync(string filter = null, string title = null, string explanation = null, string senderUserName = null, string receiverUserName = null, int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, CancellationToken cancelationToken = default)
        {
            var query = ApplyFilter(
                            (await GetQueryableAsync())
                                .Include(a => a.Sender)
                                .Include(b => b.Receiver), filter, title, explanation, senderUserName, receiverUserName);
            query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? "Title asc" : sorting);
            return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancelationToken);
        }

        protected virtual IQueryable<Request> ApplyFilter(
            IQueryable<Request> query,
              string filter = null,
             string title = null,
             string explanation = null,
             string senderUserName = null,
             string receiverUserName = null
            )
        {
            return query
                    .WhereIf(!string.IsNullOrWhiteSpace(filter),
                            e => e.Title.Contains(filter) ||
                            e.Explanation.Contains(filter) ||
                            (e.Sender != null && e.Sender.IdentityUser != null && e.Sender.IdentityUser.UserName != null ? e.Sender.IdentityUser.UserName.Contains(filter) : false) ||
                            (e.Receiver != null && e.Receiver.IdentityUser != null && e.Receiver.IdentityUser.UserName != null ? e.Receiver.IdentityUser.UserName.Contains(filter) : false)
                        )
                    .WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title))
                    .WhereIf(!string.IsNullOrWhiteSpace(explanation), e => e.Explanation.Contains(explanation))
                    .WhereIf(!string.IsNullOrWhiteSpace(senderUserName), e => e.Sender != null && e.Sender.IdentityUser != null && e.Sender.IdentityUser.UserName != null ? e.Sender.IdentityUser.UserName.Contains(senderUserName) : false)
                    .WhereIf(!string.IsNullOrWhiteSpace(receiverUserName), e => e.Receiver != null && e.Receiver.IdentityUser != null && e.Receiver.IdentityUser.UserName != null ? e.Receiver.IdentityUser.UserName.Contains(receiverUserName) : false);
        }
    }
}

